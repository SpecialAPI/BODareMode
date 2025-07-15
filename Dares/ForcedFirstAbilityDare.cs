using System;
using System.Collections.Generic;
using System.Text;

namespace BODareMode.Dares
{
    public class ForcedFirstAbilityDare : DareSO
    {
        public AbilitySO ability;
        public override object[] DescriptionArgs => [ability.GetAbilityLocData().text];
    }
}
