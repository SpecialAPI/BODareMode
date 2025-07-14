using System;
using System.Collections.Generic;
using System.Text;
using BODareMode.Dares;
using BODareMode.Serializer;
using HarmonyLib;
using MonoMod.Cil;

namespace BODareMode
{
    public class DareManager
    {
        public static DareManager Instance;
        public const string DataKey = "DareManager";
        private static readonly Dictionary<RunDataSO, DareManager> managers = [];

        public List<DareBase> dares = [];
        public bool isDareRun;

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
            dares = [new CharactersInCombatLimitDare()];
        }

        public void InitializeCombatNotifications()
        {
            if (dares == null)
                return;

            foreach(var d in dares)
                d.InitializeCombatNotifications();
        }
    }
}
