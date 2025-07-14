using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fools_RoomHandlerModData : BaseRoomHandlerModData
{
    #region EDITOR EXPOSED FIELDS
    [Header("RoomItem Locators")]
    public Basic_RoomItemModData m_FoolsSelectable = null;
    [Header("Fools Locators")]
    public SpriteRenderer[] m_FoolRenderers = null;
    #endregion

    public override string RoomHandlerType => RoomHandlerType_GameIDs.Fools.ToString();
}
