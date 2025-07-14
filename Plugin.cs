using BepInEx;
using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;

namespace BODareMode
{
    [BepInPlugin(MOD_GUID, MOD_NAME, MOD_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string MOD_GUID = "SpecialAPI.DareMode";
        public const string MOD_NAME = "Dare Mode";
        public const string MOD_VERSION = "0.0.0";
        public const string MOD_PREFIX = "DareMode";

        public static Harmony HarmonyInstance;
        public static AssetBundle Bundle;

        public void Awake()
        {
            using (var strem = Assembly.GetExecutingAssembly().GetManifestResourceStream("BODareMode.AssetBundle.AssetBundles.bodaremode"))
                Bundle = AssetBundle.LoadFromStream(strem);

            HarmonyInstance = new(MOD_GUID);
            HarmonyInstance.PatchAll();

            DareModeLossHandler.Init();
        }
    }
}
