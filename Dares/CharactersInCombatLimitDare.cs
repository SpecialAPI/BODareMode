using BODareMode.Triggers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BODareMode.Dares
{
    public class CharactersInCombatLimitDare : DareSO
    {
        public int Limit;
        public override object[] DescriptionArgs => [Limit];

        public override void InitializeCombatNotifications()
        {
            CombatManager.Instance.AddObserver(CheckCharacters, CombatTriggers.OnAddedToField);
        }

        public void CheckCharacters(object sender, object args)
        {
            if (CombatManager.Instance._stats.CharactersOnField.Count <= Limit)
                return;

            FailDare();
        }
    }
}
