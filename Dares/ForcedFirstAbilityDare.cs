using BODareMode.Triggers.Patches;
using System;
using System.Collections.Generic;
using System.Text;

namespace BODareMode.Dares
{
    public class ForcedFirstAbilityDare : DareSO
    {
        public AbilitySO ability;
        public override object[] DescriptionArgs => [ability.GetAbilityLocData().text];

        public override void InitializeCombatNotifications()
        {
            CombatManager.Instance.AddObserver(AbilityWasUsed, CombatTriggers.OnAbilityUsedContext);
        }

        public void AbilityWasUsed(object sender, object args)
        {
            if (sender is not IUnit unit || !unit.IsUnitCharacter)
                return;

            if(args is not AbilityUsedContext ctx || ctx.ability == null)
                return;

            if (ctx.ability.name == ability.name)
                return;

            var extManager = CombatManager.Instance.GetOrAddComponent<CombatManagerExt>();
            if (extManager.WasFirstAbilityUsed)
                return;

            FailDare();
            extManager.WasFirstAbilityUsed = true;
        }
    }
}
