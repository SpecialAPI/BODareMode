using System;
using System.Collections.Generic;
using System.Text;
using Tools;

namespace BODareMode
{
    public static class CustomLoc
    {
        public static string GetUIData(string id, string defaultText = "")
        {
            if (LocUtils._gameLoc._uiData.TryGetValue(id, out var uiData))
                return uiData;

            return defaultText;
        }
    }
}
