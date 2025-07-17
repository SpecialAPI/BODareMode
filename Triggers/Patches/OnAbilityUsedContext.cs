using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BODareMode.Triggers.Patches
{
    [HarmonyPatch]
    public static class OnAbilityUsedContext
    {
        private static readonly MethodInfo apc_c_te = AccessTools.Method(typeof(OnAbilityUsedContext), nameof(AbilityPerformedContext_Character_TriggerEvent));
        private static readonly MethodInfo apc_e_te = AccessTools.Method(typeof(OnAbilityUsedContext), nameof(AbilityPerformedContext_Enemy_TriggerEvent));

        [HarmonyPatch(typeof(EnemyCombat), nameof(EnemyCombat.UseAbility), typeof(int))]
        [HarmonyPatch(typeof(CharacterCombat), nameof(CharacterCombat.UseAbility), typeof(int), typeof(FilledManaCost[]))]
        [HarmonyILManipulator]
        private static void AbilityPerformedTriggers_UseAbility_Transpiler(ILContext ctx, MethodBase mthd)
        {
            var crs = new ILCursor(ctx);

            if (!crs.JumpToNext(x => x.MatchNewobj<EndAbilityAction>()))
                return;

            crs.Emit(OpCodes.Ldarg_0);
            crs.Emit(OpCodes.Ldarg_1);
            crs.Emit(OpCodes.Ldloc_0);

            if (mthd.DeclaringType == typeof(CharacterCombat))
            {
                crs.Emit(OpCodes.Ldarg_2);
                crs.Emit(OpCodes.Call, apc_c_te);
            }

            else
                crs.Emit(OpCodes.Call, apc_e_te);
        }

        private static EndAbilityAction AbilityPerformedContext_Character_TriggerEvent(EndAbilityAction curr, CharacterCombat ch, int abilityIdx, AbilitySO ability, FilledManaCost[] cost)
        {
            CombatManager.Instance.AddRootAction(new AbilityContextNotifyAction(ch, CombatTriggers.OnAbilityUsedContext, new()
            {
                ability = ability,
                abilityIndex = abilityIdx,
                cost = cost
            }));

            return curr;
        }

        private static EndAbilityAction AbilityPerformedContext_Enemy_TriggerEvent(EndAbilityAction curr, EnemyCombat en, int abilityIdx, AbilitySO ability)
        {
            CombatManager.Instance.AddRootAction(new AbilityContextNotifyAction(en, CombatTriggers.OnAbilityUsedContext, new()
            {
                ability = ability,
                abilityIndex = abilityIdx,
                cost = null
            }));

            return curr;
        }
    }

    /// <summary>
    /// Used as args by various custom triggers, contains the information about a performed ability.
    /// </summary>
    public class AbilityUsedContext
    {
        /// <summary>
        /// Index of the performed ability in the unit's abilities list.
        /// </summary>
        public int abilityIndex;
        /// <summary>
        /// The performed ability.
        /// </summary>
        public AbilitySO ability;
        /// <summary>
        /// The cost used for the ability. Null for enemies.
        /// </summary>
        public FilledManaCost[] cost;
    }

    public class AbilityContextNotifyAction(IUnit unit, string notif, AbilityUsedContext ctx) : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            CombatManager.Instance.PostNotification(notif, unit, ctx);

            yield break;
        }
    }
}
