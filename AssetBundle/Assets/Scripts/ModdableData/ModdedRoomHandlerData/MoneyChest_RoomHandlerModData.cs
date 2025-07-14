using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyChest_RoomHandlerModData : BaseRoomHandlerModData
{
    #region EDITOR EXPOSED FIELDS
    [Header("RoomItem Locators")]
    public Animated_RoomItemModData m_MoneyChestSelectable = null;
    #endregion

    public override string RoomHandlerType => RoomHandlerType_GameIDs.MoneyChest.ToString();
}
