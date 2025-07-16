using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace BODareMode
{
    public class DareListMenuHandler : MonoBehaviour
    {
        public static GameObject overworldPrefab;

        public TMP_Text topText;
        public TMP_Text bottomText;
        public List<DareUIHolder> dareHolders;

        public static void Init()
        {
            overworldPrefab = Bundle.LoadAsset<GameObject>("OverworldDareMenuHolder");
            SetUpDareList(overworldPrefab, false);
        }

        public static DareListMenuHandler SetUpDareList(GameObject go, bool lossSequence)
        {
            var menu = go.AddComponent<DareListMenuHandler>();
            var paper = go.transform.Find("Paper");

            menu.topText = paper.Find("Top Text").GetComponent<TMP_Text>();
            menu.bottomText = paper.Find("Bottom Text").GetComponent<TMP_Text>();

            menu.dareHolders = [];
            var dareList = paper.Find("DareListContainer");
            for (var i = 0; i < dareList.childCount; i++)
            {
                var dareContainer = dareList.GetChild(i);

                if (dareContainer == null)
                    continue;

                var uiHolder = dareContainer.gameObject.AddComponent<DareUIHolder>();
                uiHolder.titleText = dareContainer.Find("Title").GetComponent<TMP_Text>();
                uiHolder.descriptionText = dareContainer.Find("Description").GetComponent<TMP_Text>();
                uiHolder.index = i;

                var red = uiHolder.titleText.color; 
                var black = uiHolder.descriptionText.color;
                uiHolder.normalTitleColor = lossSequence ? black : red;
                uiHolder.failedTitleColor = red;
                uiHolder.normalDescColor = black;
                uiHolder.failedDescColor = red;

                menu.dareHolders.Add(uiHolder);
            }

            return menu;
        }

        public void ShowMenu()
        {
            gameObject.SetActive(true);
        }

        public void HideMenu()
        {
            gameObject.SetActive(false);
        }

        public void SetInformation(int failedDare = -1)
        {
            topText.text = CustomLoc.GetUIData(CustomUILoc.DarePageTopTextID, CustomUILoc.DarePageTopTextDefault);
            bottomText.text = CustomLoc.GetUIData(CustomUILoc.DarePageBottomTextID, CustomUILoc.DarePageBottomTextDefault);

            if (DareManager.Instance == null)
                return;

            for(var i = 0; i < dareHolders.Count; i++)
            {
                var dareHolder = dareHolders[i];

                if (dareHolder == null)
                    continue;

                if (i >= DareManager.Instance.daresForRun.Count)
                {
                    dareHolder.gameObject.SetActive(false);
                    continue;
                }

                dareHolder.gameObject.SetActive(true);
                dareHolder.SetInformation(i < DareManager.Instance.activeDares.Count ? DareManager.Instance.activeDares[i] : null, i == failedDare);
            }
        }
    }
}
