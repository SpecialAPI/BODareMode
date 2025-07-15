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

        public void SetInformation(DareSO dare)
        {
            var titleFormat = CustomLoc.GetUIData(CustomUILoc.DareTitleID, CustomUILoc.DareTitleDefault);
            titleText.text = string.Format(titleFormat, index + 1);

            if(dare != null)
                descriptionText.text = dare.GetDescription();
            else
                descriptionText.text = string.Empty;
        }
    }
}
