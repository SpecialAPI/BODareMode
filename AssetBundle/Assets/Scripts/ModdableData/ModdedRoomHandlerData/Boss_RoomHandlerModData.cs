using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_RoomHandlerModData : BaseRoomHandlerModData
{
    #region EDITOR EXPOSED FIELDS
    [Header("RoomItem Locators")]
    public Basic_RoomItemModData m_BossPortalSelectable = null;
    public Basic_RoomItemModData m_ZonePortalSelectable = null;
    public BaseRoomItemModData m_ExtraSelectable = null;
    [Header("Group Locators")]
    public GameObject m_BossPortalHolder = null;
    public GameObject m_ZonePortalHolder = null;

    [Header("Individual Renderers")]
    public SpriteRenderer m_BossPortalRenderer = null;
    public SpriteRenderer m_ZonePortalRenderer = null;
    #endregion

    public override string RoomHandlerType => RoomHandlerType_GameIDs.Boss.ToString();
}
