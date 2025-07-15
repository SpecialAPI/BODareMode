using System;
using System.Collections.Generic;
using System.Text;

namespace BODareMode.Dares
{
    public class TurnTimeLimitDare : DareSO
    {
        public float time;
        public override object[] DescriptionArgs => [time];
    }
}
