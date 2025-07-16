using System;
using System.Collections.Generic;
using System.Text;

namespace BODareMode
{
    public static class CustomUILoc
    {
        public const string DareTitleID = $"{MOD_PREFIX}_DareTitle";
        public const string DareTitleDefault = "DARE #{0}:";

        public const string DarePageTopTextID = $"{MOD_PREFIX}_DarePageTopText";
        public const string DarePageTopTextDefault = "I dare you to complete a run without failing any of the following DARES.";
        public const string DarePageBottomTextID = $"{MOD_PREFIX}_DarePageBottomText";
        public const string DarePageBottomTextDefault = "At the end of each area, a new dare will be revealed and added to the above dare list. If even a single dare is broken, the run will be instantly lost.";

        public const string DareLossSequence1ID = $"{MOD_PREFIX}_DareLossSequence1";
        public const string DareLossSequence1Default = "And I will say goodbye...";
        public const string DareLossSequence2ID = $"{MOD_PREFIX}_DareLossSequence2";
        public const string DareLossSequence2Default = "...to my dreams and the sea.";
        public const string DareLossSequence3ID = $"{MOD_PREFIX}_DareLossSequence3";
        public const string DareLossSequence3Default = "And wonder why daring...";
        public const string DareLossSequence4ID = $"{MOD_PREFIX}_DareLossSequence4";
        public const string DareLossSequence4Default = "...feels just like defeat.";

        public const string GameOverID = $"{MOD_PREFIX}_GameOver";
        public const string GameOverDefault = "Game Over";
        public const string ReturnToMainMenuID = $"{MOD_PREFIX}_ReturnToMainMenu";
        public const string ReturnToMainMenuDefault = "return to\n<size=200%>MAIN MENU</size>";
    }
}
