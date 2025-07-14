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
        public static void OnCharacterAddedToField_Postfix(CombatStats __instance, int characterID)
        {
            var ch = __instance.Characters[characterID];
            CombatManager.Instance.PostNotification(CustomTriggers.OnAddedToField, ch, null);
        }
    }
}
