using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BODareMode
{
    public abstract class DareBase
    {
        public DareManager manager;

        public void FailDare()
        {
            CombatManager.Instance.AddRootAction(new LoseDareAction());
        }

        public virtual void InitializeCombatNotifications()
        {
        }

        public virtual void InitializeOverworldNotifications()
        {
        }

        public abstract string GetDescription();

        public string FormatAndLocalizeDescription(string locId, string defaultDesc, params object[] args)
        {
            var format = CustomLoc.GetUIData(locId, defaultDesc);

            if(args.Length <= 0)
                return format;

            return string.Format(format, args);
        }
    }
}
