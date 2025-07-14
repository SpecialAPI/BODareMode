using BODareMode.Triggers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BODareMode.Dares
{
    public class CharactersInCombatLimitDare : DareBase
    {
        public const string LocId = $"{Plugin.MOD_PREFIX}_{nameof(CharactersInCombatLimitDare)}";
        public const string DefaultDesc = "I dare you to never have more than {0} party members in combat.";

        public static readonly int Limit = 4;

        public override void InitializeCombatNotifications()
        {
            CombatManager.Instance.AddObserver(CheckCharacters, CustomTriggers.OnAddedToField);
        }

        public void CheckCharacters(object sender, object args)
        {
            if (CombatManager.Instance._stats.CharactersOnField.Count <= Limit)
                return;

            FailDare();
        }

        public override string GetDescription()
        {
            return FormatAndLocalizeDescription(LocId, DefaultDesc, Limit);
        }
    }
}
