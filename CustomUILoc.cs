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
    }
}
