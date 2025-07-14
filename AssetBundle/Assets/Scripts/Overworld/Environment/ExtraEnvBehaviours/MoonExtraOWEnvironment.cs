using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonExtraOWEnvironment : ExtraEnvironmentBehaviour
{
    public Sprite _moonChild = null;
    public SpriteRenderer[] _moonRenderers = null;
    public string m_MoonChildActivator = "IchorQuest4";

    public override void SetUpExtraEnvironment(IGameCheckData gameCheckDB)
    {
        if (gameCheckDB == null || gameCheckDB.Equals(null))
            return;
        if (gameCheckDB.DidCompleteQuest(m_MoonChildActivator))
        {
            foreach (SpriteRenderer item in _moonRenderers)
                item.sprite = _moonChild;
        }
    }

}
