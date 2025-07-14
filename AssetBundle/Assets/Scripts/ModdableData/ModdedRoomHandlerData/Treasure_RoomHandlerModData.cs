using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure_RoomHandlerModData : BaseRoomHandlerModData
{
    #region EDITOR EXPOSED FIELDS
    [Header("RoomItem Locators")]
    public Animated_RoomItemModData m_TreasureSelectable = null;
    #endregion

    public override string RoomHandlerType => RoomHandlerType_GameIDs.Treasure.ToString();
}
