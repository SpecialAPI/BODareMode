using UnityEngine;

public class EnemyInFieldLayout_Data : MonoBehaviour
{
    #region EDITOR EXPOSED FIELDS
    [Header("References")]
    public Transform m_RootTransform = null;
    public GameObject m_Locator = null;
    public SpriteRenderer m_Renderer = null;
    public Animator m_Animator = null;
    [Header("UI References")]
    public Transform m_UI3DLocation = null;

    [Header("Data")]
    public ParticleSystem m_Gibs = null;

    [Header("Audio Data")]
    public string m_GibsEvent = "";
    public string[] m_ExtraSounds = null;

    [Header("Animation Data")]
    public float m_SaveAnimationTime = 2f;
    #endregion

    public void SetDefaultData()
    {
        m_RootTransform = transform;
        m_Locator = m_RootTransform.Find("Locator").gameObject;
        m_Renderer = GetComponentInChildren<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();

        m_UI3DLocation = m_RootTransform.Find("3DUILocation").transform;

        m_GibsEvent = "event:/Combat/Gibs/CBT_Gibs";
        m_SaveAnimationTime = 2;
        m_ExtraSounds = new string[0];
    }
}
