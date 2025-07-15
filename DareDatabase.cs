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
            max4CombatCharsDare.name = $"{MOD_PREFIX}_DareCombatCharacterLimit_4";
            max4CombatCharsDare.defaultDescription = "I dare you to never have more than {0} characters in combat.";
            max4CombatCharsDare.Limit = 4;
            TryAddNewDare(max4CombatCharsDare);

            var forcedSlapFirstAbilityDare = CreateScriptable<ForcedFirstAbilityDare>();
            forcedSlapFirstAbilityDare.name = $"{MOD_PREFIX}_DareForcedFirstAbility_Slap";
            forcedSlapFirstAbilityDare.defaultDescription = "I dare you to start each combat by using {0}.";
            forcedSlapFirstAbilityDare.ability = LoadedAssetsHandler.GetCharacterAbility("Slap_A");
            TryAddNewDare(forcedSlapFirstAbilityDare);

            var requiredSlapBossKillDare = CreateScriptable<RequiredFinishingHitDeathTypeDare>();
            requiredSlapBossKillDare.name = $"{MOD_PREFIX}_DareRequiredBossKill_Slap";
            requiredSlapBossKillDare.defaultDescription = "I dare you to kill the last enemy in each boss encounter by using Slap.";
            requiredSlapBossKillDare.deathType = DeathType_GameIDs.Slap.ToString();
            requiredSlapBossKillDare.bossesOnly = true;
            TryAddNewDare(requiredSlapBossKillDare);

            var turnTimeLimit30SecondsDare = CreateScriptable<TurnTimeLimitDare>();
            turnTimeLimit30SecondsDare.name = $"{MOD_PREFIX}_DareTurnTimeLimit_30Seconds";
            turnTimeLimit30SecondsDare.defaultDescription = "I dare you to take no more than 30 seconds doing nothing on your turn.";
            turnTimeLimit30SecondsDare.time = 30f;
            TryAddNewDare(turnTimeLimit30SecondsDare);
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
