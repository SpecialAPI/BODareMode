using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace BODareMode
{
    public class DareUIHolder : MonoBehaviour
    {
        public TMP_Text titleText;
        public TMP_Text descriptionText;
        public int index;

        public Color normalTitleColor;
        public Color failedTitleColor;
        public Color normalDescColor;
        public Color failedDescColor;

        public void SetInformation(DareSO dare, bool failed = false)
        {
            var titleFormat = CustomLoc.GetUIData(CustomUILoc.DareTitleID, CustomUILoc.DareTitleDefault);
            titleText.text = string.Format(titleFormat, index + 1);
            titleText.color = failed ? failedTitleColor : normalTitleColor;

            descriptionText.text = dare != null ? dare.GetDescription() : string.Empty;
            descriptionText.color = failed ? failedDescColor : normalDescColor;
        }
    }
}
