using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BODareMode
{
    public class LoseDareAction : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            CombatManager.Instance.GetOrAddComponent<CombatManagerExt>().IsDareModeLost = true;
            stats.TriggerPrematureFinalization();

            yield break;
        }
    }
}
