using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BODareMode
{
    [HarmonyPatch]
    public static class DareManagerPatches
    {
        public static MethodInfo edmonr_e = AccessTools.Method(typeof(DareManagerPatches), nameof(EnableDareManagerOnNewRun_Enable));
        public static MethodInfo idcn_i = AccessTools.Method(typeof(DareManagerPatches), nameof(InitializeDareCombatNotifications_Initialize));

        [HarmonyPatch(typeof(GameInformationHolder), nameof(GameInformationHolder.PrepareGameRun))]
        [HarmonyPostfix]
        public static void LoadDareManager_Postfix(RunDataSO run)
        {
            if (run == null)
                return;

            DareManager.LoadRun(run);
        }

        [HarmonyPatch(typeof(SaveDataManager_2024), nameof(SaveDataManager_2024.FullySaveGameDataToCache))]
        [HarmonyPrefix]
        public static void SaveDareManager_Prefix(RunDataSO run)
        {
            if(run == null)
                return;

            if(DareManager.TryGetManager(run, out var manager))
                manager.Save();
        }

        [HarmonyPatch(typeof(MainMenuController), nameof(MainMenuController.PrepareNewRunData), MethodType.Enumerator)]
        [HarmonyILManipulator]
        public static void EnableDareManagerOnNewRun_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if (!crs.JumpToNext(x => x.MatchCallOrCallvirt<GameInformationHolder>(nameof(GameInformationHolder.PrepareGameRun))))
                return;

            crs.Emit(OpCodes.Call, edmonr_e);
        }

        public static void EnableDareManagerOnNewRun_Enable()
        {
            DareManager.Instance?.OnNewRunStarted();
        }

        [HarmonyPatch(typeof(CombatManager), nameof(CombatManager.InitializeCombat))]
        [HarmonyILManipulator]
        public static void InitializeDareCombatNotifications_Transpiler(ILContext ctx)
        {
            var crs = new ILCursor(ctx);

            if(!crs.JumpToNext(x => x.MatchCallOrCallvirt<CombatStats>(nameof(CombatStats.SetUpNotifications))))
                return;

            crs.Emit(OpCodes.Call, idcn_i);
        }

        public static void InitializeDareCombatNotifications_Initialize()
        {
            DareManager.Instance?.InitializeCombatNotifications();
        }
    }
}
