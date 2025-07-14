using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRoomItemModData : MonoBehaviour
{
    [Header("Room Item")]
    public SpriteRenderer[] m_ConnectedRenderers = null;
    public BoxCollider2D m_Detector = null;
    public abstract string RoomItemType { get; }
}

public enum RoomItemType_GameIDs
{
    Basic,
    Animated,
    MandatoryNPC,
    CustomDialogue,
    CustomDialogueByQuest
}
