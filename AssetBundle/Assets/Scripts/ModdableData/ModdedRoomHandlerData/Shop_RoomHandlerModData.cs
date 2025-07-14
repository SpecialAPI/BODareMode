using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_RoomHandlerModData : BaseRoomHandlerModData
{
    #region EDITOR EXPOSED FIELDS
    [Header("RoomItem Locators")]
    public Basic_RoomItemModData m_ShopSelectable = null;
    #endregion

    public override string RoomHandlerType => RoomHandlerType_GameIDs.Shop.ToString();
}
