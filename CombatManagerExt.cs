using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using HarmonyLib;
using MonoMod.Cil;
using System.Reflection;
using Mono.Cecil.Cil;
using System.Collections;
using BrutalAPI;
using DG.Tweening;

namespace BODareMode
{
    [HarmonyPatch]
    public class CombatManagerExt : MonoBehaviour
    {
        public bool IsDareModeLost;
        public CombatManager manager;
        public DareModeLossHandler lossHandler;

        public void Awake()
        {
            manager = GetComponent<CombatManager>();

            lossHandler = Instantiate(DareModeLossHandler.prefab, manager._gameOverPanel.transform.parent).GetComponent<DareModeLossHandler>();
            lossHandler.transform.SetAsFirstSibling();
            lossHandler.fadeHandler = manager._fadeHandler;
        }

        public IEnumerator DoDareModeGameOver()
        {
            var combatEndWaitTimer = LoadedDBsHandler.CombatData.CombatEndWaitTimer;
            yield return new WaitForSeconds(3f);

            yield return lossHandler.DoLossDialogue();

            manager._soundManager.SetPauseData(false, false, false);
            DOTween.KillAll();

            yield break;
        }

        public static MethodInfo ddmogo_oc = AccessTools.Method(typeof(CombatManagerExt), nameof(DoDareModeOverrideGameOver_OverrideCheck));
        public static MethodInfo ddmogo_os = AccessTools.Method(typeof(CombatManagerExt), nameof(DoDareModeOverrideGameOver_OverrideSequence));

        [HarmonyPatch(typeof(CombatManager), nameof(CombatManager.CombatFlow), MethodType.Enumerator)]
        [HarmonyILManipulator]
        public static void DoDareModeOverrideGameOver_Transpiler(ILContext ctx, MethodBase mthd)
        {
            var crs = new ILCursor(ctx);
            var current = mthd.EnumeratorField("current");

            if (!crs.JumpToNext(x => x.MatchStloc(7)))
                return;

            crs.Emit(OpCodes.Ldloc, 1);
            crs.Emit(OpCodes.Call, ddmogo_oc);

            if(!crs.JumpToNext(x => x.MatchStfld(current)))
                return;

            crs.Emit(OpCodes.Ldarg, 0);
            crs.Emit(OpCodes.Ldloc, 1);
            crs.Emit(OpCodes.Call, ddmogo_os);
        }

        public static bool DoDareModeOverrideGameOver_OverrideCheck(bool curr, CombatManager cm)
        {
            if (cm == null || cm.GetComponent<CombatManagerExt>() is not CombatManagerExt cmExt)
                return curr;

            if (!cmExt.IsDareModeLost)
                return curr;

            return false;
        }

        public static void DoDareModeOverrideGameOver_OverrideSequence(IEnumerator e, CombatManager cm)
        {
            if(cm == null || cm.GetComponent<CombatManagerExt>() is not CombatManagerExt cmExt)
                return;

            if (!cmExt.IsDareModeLost)
                return;

            e.EnumeratorSetField("current", cmExt.DoDareModeGameOver());
        }
    }
}
