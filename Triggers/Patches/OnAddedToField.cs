using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace BODareMode.Triggers.Patches
{
    [HarmonyPatch]
    public static class OnAddedToField
    {
        [HarmonyPatch(typeof(CombatStats), nameof(CombatStats.AddCharacterToField))]
        [HarmonyPostfix]
        public static void OnAddedToField_Character_Postfix(CombatStats __instance, int characterID)
        {
            var ch = __instance.Characters[characterID];
            CombatManager.Instance.PostNotification(CombatTriggers.OnAddedToField, ch, null);
        }

        [HarmonyPatch(typeof(CombatStats), nameof(CombatStats.AddEnemyToField))]
        [HarmonyPostfix]
        public static void OnAddedToField_Enemy_Postfix(CombatStats __instance, int enemyID)
        {
            var ch = __instance.Enemies[enemyID];
            CombatManager.Instance.PostNotification(CombatTriggers.OnAddedToField, ch, null);
        }
    }
}
