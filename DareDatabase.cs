using BODareMode.Dares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BODareMode
{
    public static class DareDatabase
    {
        public static Dictionary<string, DareSO> dares = [];

        public static void Init()
        {
            var max4CombatCharsDare = CreateScriptable<CharactersInCombatLimitDare>();
            max4CombatCharsDare.name = $"{MOD_PREFIX}_CombatCharacterLimit_4";
            max4CombatCharsDare.defaultDescription = "I dare you to never have more than {0} characters in combat.";
            max4CombatCharsDare.Limit = 4;
            TryAddNewDare(max4CombatCharsDare);
        }

        public static List<DareSO> GetDares(int amount)
        {
            var pool = dares.Values.ToList();
            var output = new List<DareSO>();

            for(var i = 0; (i < amount) && (pool.Count > 0); i++)
            {
                var idx = Random.Range(0, pool.Count);
                var d = pool[idx];

                if (d == null)
                    continue;

                pool.RemoveAt(idx);
                output.Add(d);
            }

            return output;
        }

        public static bool TryAddNewDare(DareSO dare)
        {
            var id = dare.name;

            if (dares.ContainsKey(id))
                return false;

            dares[id] = dare;
            return true;
        }

        public static DareSO GetDare(string name)
        {
            if(dares.TryGetValue(name, out DareSO dareSO))
                return dareSO;

            return null;
        }
    }
}
