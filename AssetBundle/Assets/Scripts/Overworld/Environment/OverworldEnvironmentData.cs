using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldEnvironmentData : MonoBehaviour
{
    #region EDITOR EXPOSED FIELDS
    [Header("World Data")]
    public Transform m_WorldGround = null;
    public float m_HeightFromWorldGroundCenter = 100;

    [Header("Extra Data")]
    public ExtraEnvironmentBehaviour m_ExtraBehaviour = null;
    [Header("Prop Data")]
    public Transform m_PropLocator = null;
    public OverworldPropLineHandler m_PropLineTemplate = null;
    public SpriteRenderer m_PropTemplate = null;
    public Sprite[] m_SelectableProps = null;
    public bool m_UseCustomFlip = false;
    public bool m_LeftCustomFlipX = false;
    public bool m_RightCustomFlipX = true;
    public Vector2 m_InternalLimits = Vector2.one; //-6, 6
    public Vector2 m_AngleLimits = Vector2.one; //25, -5
    public float m_PropWidth = 7;
    public float m_PropLineAngle = 7;
    [Range(0, 1)] public float m_PropSideOffset = .5f;
    [Range(0, 1)] public float m_PropAngleOffset = .5f;
    public int m_PropSideCount = 5;
    [Range(0, 1)] public float m_PropSpawnChance = .25f;

    [Header("World Moving Data (Don't touch these ones if you don't know what you are doing)")]
    public AnimationCurve m_SpeedCurve = null;
    public float m_MoveAngle = -25;
    public float m_MoveTime = 4;
    public Vector3 m_GroundCenterPosition = Vector3.zero;
    public float m_GroundVerticalOffset = -.05f;
    public Vector3 m_WorldRotation = Vector3.right;
    public Vector3 m_WorldUp = Vector3.up;

    [Header("Character and Room Positioning Data (Same as before)")]
    public float m_CharacterOffsetAngle = -1.25f;
    public float m_RoomAngle = 25;
    #endregion

    #region PROPERTIES
    public bool HasPropData { get => !(m_SelectableProps == null || m_SelectableProps.Length <= 0); }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 spawnSize = new Vector3(m_PropWidth, 2, m_PropLineAngle);
        Vector3 offsetSize = new Vector3(m_PropSideOffset * m_PropWidth, 1, m_PropAngleOffset * m_PropLineAngle);
        Vector3 center = m_GroundCenterPosition;
        for (int i = 0; i < m_PropSideCount; i++)
        {
            center.x = m_InternalLimits.x - m_PropWidth * i - m_PropWidth * .5f;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, spawnSize);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(center, offsetSize);

            center.x = m_InternalLimits.y + m_PropWidth * i + m_PropWidth * .5f;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, spawnSize);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(center, offsetSize);
        }
    }
}
