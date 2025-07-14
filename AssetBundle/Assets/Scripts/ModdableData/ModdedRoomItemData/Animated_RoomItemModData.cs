using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animated_RoomItemModData : BaseRoomItemModData
{
    [Header("Audio")]
    public string m_AnimationSound = "";

    [Header("Animation")]
    public Animator m_Animator = null;
    public string m_AnimationTrigger = null;
    public string m_AnimationEndStateName = null;
    public bool m_DisableOnFinish = true;
    public float m_SafeWaitTime = 1;

    public override string RoomItemType => RoomItemType_GameIDs.Animated.ToString();
}
