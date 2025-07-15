using FMODUnity;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BODareMode
{
    [HarmonyPatch]
    public class CustomOverworldUIHandler : MonoBehaviour
    {
        public OverworldMainUIHandler uiHandler;
        public DareListMenuHandler dareListMenu;

        public void Initialization()
        {
            uiHandler = GetComponent<OverworldMainUIHandler>();

            dareListMenu = Instantiate(DareListMenuHandler.overworldPrefab, transform).GetComponent<DareListMenuHandler>();
            dareListMenu.gameObject.SetActive(false);
        }

        public void OpenDareListMenu()
        {
            dareListMenu.ShowMenu();
            dareListMenu.SetInformation();

            RuntimeManager.PlayOneShot(uiHandler._openMenuEvent);
            gameObject.SetActive(true);
            uiHandler.SetMenuControl(true);
        }

        public void CloseMenu()
        {
            dareListMenu.HideMenu();
        }

        [HarmonyPatch(typeof(OverworldMainUIHandler), nameof(OverworldMainUIHandler.InitializeMenuData))]
        [HarmonyPostfix]
        public static void InitializeMenuData_Postfix(OverworldMainUIHandler __instance)
        {
            __instance.CustomUIHandler();
        }

        [HarmonyPatch(typeof(OverworldMainUIHandler), nameof(OverworldMainUIHandler.CloseMenu))]
        [HarmonyPostfix]
        public static void CloseMenu_Postfix(OverworldMainUIHandler __instance)
        {
            __instance.CustomUIHandler().CloseMenu();
        }
    }

    public static class CustomOverworldUIExtensions
    {
        public static CustomOverworldUIHandler CustomUIHandler(this OverworldMainUIHandler self)
        {
            var handler = self.GetComponent<CustomOverworldUIHandler>();
            if (handler)
                return handler;

            handler = self.AddComponent<CustomOverworldUIHandler>();
            handler.Initialization();
            return handler;
        }
    }
}
