using System;
using System.Collections.Generic;
using System.Text;
using BODareMode.Dares;
using BODareMode.Serializer;
using HarmonyLib;
using MonoMod.Cil;
using UnityEngine;

namespace BODareMode
{
    public class DareManager
    {
        public static DareManager Instance;
        public const string DataKey = "DareManager";
        private static readonly Dictionary<RunDataSO, DareManager> managers = [];

        public List<DareSO> daresForRun = [];
        public List<DareSO> activeDares = [];
        public int revealedDares;
        public bool isDareRun;

        public static readonly int DareAmountPerRun = 4;

        [RDSDontSerialize]
        public RunDataSO run;

        public static bool TryGetManager(RunDataSO run, out DareManager manager)
        {
            if (managers.TryGetValue(run, out manager))
                return true;

            manager = null;
            return false;
        }

        public static void LoadRun(RunDataSO run)
        {
            if (!managers.TryGetValue(run, out var manager))
            {
                if (!RunDataSerializer.TryDeserializeFromRun(DataKey, run.inGameData, out manager))
                    manager = new();

                managers[run] = manager;
                manager.run = run;
            }

            Instance = manager;
        }

        public void Save()
        {
            RunDataSerializer.SerializeToRun(this, DataKey, run.inGameData);
        }

        public void OnNewRunStarted()
        {
            isDareRun = true;
            daresForRun.AddRange(DareDatabase.GetDares(DareAmountPerRun));

            revealedDares = 0;
            TryAddNextDare();
        }

        public bool TryAddNextDare()
        {
            if (revealedDares >= daresForRun.Count)
                return false;

            revealedDares++;
            UpdateActiveDares();
            return true;
        }

        public void UpdateActiveDares()
        {
            if(activeDares == null || daresForRun == null)
                return;

            for(var i = activeDares.Count; i < revealedDares; i++)
            {
                if(i >= daresForRun.Count)
                {
                    Debug.LogError("Dare Mode: revealedDares is higher than daresForRun.Count");
                    break;
                }

                activeDares.Add(daresForRun[i]);
            }
        }

        public void InitializeCombatNotifications()
        {
            if (activeDares == null)
                return;

            foreach(var d in activeDares)
                d.InitializeCombatNotifications();
        }
    }
}
