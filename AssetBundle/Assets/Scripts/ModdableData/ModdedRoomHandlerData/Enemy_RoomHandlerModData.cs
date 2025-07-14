using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RoomHandlerModData : BaseRoomHandlerModData
{
    #region EDITOR EXPOSED FIELDS
    [Header("RoomItem Locators")]
    public Basic_RoomItemModData[] m_EnemySelectables = null;
    [Header("Group Locators")]
    public GameObject m_EnemyGang = null;
    public GameObject m_CorpseGang = null;

    [Header("Individual Renderers")]
    public SpriteRenderer[] m_EnemyRenderers = null;
    public SpriteRenderer[] m_CorpseRenderers = null;
    #endregion

    public override string RoomHandlerType => RoomHandlerType_GameIDs.Enemy.ToString();
}
