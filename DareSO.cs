using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BODareMode
{
    public abstract class DareSO : ScriptableObject
    {
        public string defaultDescription;
        public virtual object[] DescriptionArgs => null;

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

        public virtual string GetDescription()
        {
            return FormatAndLocalizeDescription(name, defaultDescription, DescriptionArgs);
        }

        public string FormatAndLocalizeDescription(string locId, string defaultDesc, params object[] args)
        {
            var format = CustomLoc.GetUIData(locId, defaultDesc);

            if(args == null || args.Length <= 0)
                return format;

            return string.Format(format, args);
        }
    }
}
