using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace BODareMode
{
    public class DareModeLossHandler : MonoBehaviour
    {
        public TMP_Text text;
        public BlackScreenHandler fadeHandler;

        public static GameObject prefab;

        public static void Init()
        {
            prefab = Plugin.Bundle.LoadAsset<GameObject>("DareModeLossHolder");
            var transform = prefab.transform;

            foreach (var text in prefab.GetComponentsInChildren<TMP_Text>(true))
                text.font = LoadedDBsHandler.LocalisationDB.m_DefaultGameFont;

            var handler = prefab.AddComponent<DareModeLossHandler>();
            handler.text = transform.Find("Text (TMP)").GetComponent<TMP_Text>();
        }

        public IEnumerator DoLossDialogue()
        {
            text.transform.gameObject.SetActive(true);
            RuntimeManager.PlayOneShot("event:/Characters/Player/Nowak/CHR_PLR_Nowak_Dx_Sad");
            text.text = "And I will say goodbye...";
            yield return new WaitForSeconds(2f);

            text.transform.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);

            RuntimeManager.PlayOneShot("event:/Characters/Player/Nowak/CHR_PLR_Nowak_Dx_Sad");
            text.text = "...to my dreams and the sea.";
            yield return new WaitForSeconds(1f);
            fadeHandler.FadeInBlackScreen();
            while (fadeHandler.IsBlackScreenInTransition)
                yield return null;

            text.transform.gameObject.SetActive(false);
            fadeHandler.FadeOutBlackScreen(0.2f);

            text.transform.gameObject.SetActive(false);
        }
    }
}
