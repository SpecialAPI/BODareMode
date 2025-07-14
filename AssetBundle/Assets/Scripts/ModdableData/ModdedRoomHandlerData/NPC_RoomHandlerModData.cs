using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_RoomHandlerModData : BaseRoomHandlerModData
{
    #region EDITOR EXPOSED FIELDS
    [Header("Data")]
    public bool m_RequiresToTalk = false;
    public string m_dialogueMusicEvent = "";
    [Header("RoomItem Locators")]
    public BaseRoomItemModData m_NpcSelectable = null;
    public BaseRoomItemModData m_ExtraSelectable = null;
    #endregion

    public override string RoomHandlerType => RoomHandlerType_GameIDs.NPC.ToString();
}
