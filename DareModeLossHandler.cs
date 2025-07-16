using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BODareMode
{
    public class DareModeLossHandler : MonoBehaviour
    {
        public static GameObject prefab;

        public TMP_Text bigText;
        public TMP_Text smallText;
        public Image lowerBlackScreen;
        public DareListMenuHandler dareList;
        public BlackScreenHandler fadeHandler;
        public GameObject gameOverScreen;
        public TMP_Text[] gameOverTexts;
        public Button mainMenuButton;
        public TMP_Text mainMenuText;

        private bool mainMenuButtonPressed;

        public static void Init()
        {
            prefab = Bundle.LoadAsset<GameObject>("DareModeLossHolder");
            var transform = prefab.transform;
            var defaultFont = LoadedDBsHandler.LocalisationDB.m_DefaultGameFont;

            var handler = prefab.AddComponent<DareModeLossHandler>();
            handler.bigText = transform.Find("BigText").GetComponent<TMP_Text>();
            handler.bigText.font = defaultFont;
            handler.smallText = transform.Find("SmallText").GetComponent<TMP_Text>();
            handler.smallText.font = defaultFont;
            handler.lowerBlackScreen = transform.Find("LowerBlackScreen").GetComponent<Image>();

            var dareListGo = transform.Find("OverworldDareMenuHolder").gameObject;
            handler.dareList = DareListMenuHandler.SetUpDareList(dareListGo, true);

            var gameOverTransform = transform.Find("GameOverScreenHandler");
            handler.gameOverScreen = gameOverTransform.gameObject;
            handler.gameOverTexts = [gameOverTransform.Find("GameOverTextReflection").GetComponent<TMP_Text>(), gameOverTransform.Find("GameOverText").GetComponent<TMP_Text>()];
            handler.mainMenuButton = gameOverTransform.Find("MainMenuText").GetComponent<Button>();
            handler.mainMenuText = gameOverTransform.Find("MainMenuText").GetComponent<TMP_Text>();
        }

        public void Awake()
        {
            mainMenuButton.onClick.AddListener(MainMenuPressed);
        }

        public void MainMenuPressed()
        {
            mainMenuButtonPressed = true;
        }

        public IEnumerator DoLossDialogue()
        {
            bigText.transform.gameObject.SetActive(true);
            RuntimeManager.PlayOneShot("event:/Characters/Player/Nowak/CHR_PLR_Nowak_Dx_Sad");
            bigText.text = CustomLoc.GetUIData(CustomUILoc.DareLossSequence1ID, CustomUILoc.DareLossSequence1Default);
            yield return new WaitForSeconds(2f);

            bigText.transform.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);

            bigText.transform.gameObject.SetActive(true);
            RuntimeManager.PlayOneShot("event:/Characters/Player/Nowak/CHR_PLR_Nowak_Dx_Sad");
            bigText.text = CustomLoc.GetUIData(CustomUILoc.DareLossSequence2ID, CustomUILoc.DareLossSequence2Default);

            fadeHandler.FadeInBlackScreen(4f);
            while (fadeHandler.IsBlackScreenInTransition)
                yield return null;

            bigText.transform.gameObject.SetActive(false);
            lowerBlackScreen.gameObject.SetActive(true);

            dareList.ShowMenu();
            dareList.SetInformation(0);

            var dareListTransform = dareList.transform as RectTransform;
            var scale1 = new Vector3(0.75f, 0.75f, 1f);
            var scale2 = new Vector3(1.35f, 1.35f, 1f);
            dareListTransform.localScale = scale1;

            StartCoroutine(ZoomInDareList(dareListTransform, scale1, scale2, 12f));
            fadeHandler.FadeOutBlackScreen(2f);
            while (fadeHandler.IsBlackScreenInTransition)
                yield return null;

            smallText.transform.gameObject.SetActive(true);
            RuntimeManager.PlayOneShot("event:/Characters/Player/Nowak/CHR_PLR_Nowak_Dx_Sad");
            smallText.text = CustomLoc.GetUIData(CustomUILoc.DareLossSequence3ID, CustomUILoc.DareLossSequence3Default);
            yield return new WaitForSeconds(3f);

            smallText.transform.gameObject.SetActive(false);
            yield return new WaitForSeconds(2f);

            smallText.transform.gameObject.SetActive(true);
            RuntimeManager.PlayOneShot("event:/Characters/Player/Nowak/CHR_PLR_Nowak_Dx_Sad");
            smallText.text = CustomLoc.GetUIData(CustomUILoc.DareLossSequence4ID, CustomUILoc.DareLossSequence4Default);

            fadeHandler.FadeInBlackScreen(4f);
            while (fadeHandler.IsBlackScreenInTransition)
                yield return null;

            dareList.HideMenu();
            smallText.gameObject.SetActive(false);

            gameOverScreen.SetActive(true);
            var gameOver = CustomLoc.GetUIData(CustomUILoc.GameOverID, CustomUILoc.GameOverDefault);

            foreach (var txt in gameOverTexts)
                txt.text = gameOver;

            mainMenuText.text = CustomLoc.GetUIData(CustomUILoc.ReturnToMainMenuID, CustomUILoc.ReturnToMainMenuDefault);

            fadeHandler.FadeOutBlackScreen(2f);
            while (fadeHandler.IsBlackScreenInTransition)
                yield return null;

            yield return new WaitForSeconds(5f);

            var col1 = new Color(1f, 1f, 1f, 0f);
            var col2 = Color.white;
            var time = 1.5f;

            for(var ela = 0f; ela < 1.5f; ela += Time.deltaTime)
            {
                mainMenuText.color = Color.Lerp(col1, col2, ela / time);
                yield return null;
            }

            mainMenuText.color = col2;
            mainMenuButton.interactable = true;

            while (!mainMenuButtonPressed)
                yield return null;

            mainMenuButton.gameObject.SetActive(false);

            fadeHandler.FadeInBlackScreen(4f);
            while (fadeHandler.IsBlackScreenInTransition)
                yield return null;
        }

        public IEnumerator ZoomInDareList(RectTransform transform, Vector3 scale1, Vector3 scale2, float time)
        {
            for(var ela = 0f; ela < time; ela += Time.deltaTime)
            {
                transform.localScale = Vector3.Lerp(scale1, scale2, Ease.QuadIn(ela / time));
                yield return null;
            }

            transform.localScale = scale2;
        }
    }
}
