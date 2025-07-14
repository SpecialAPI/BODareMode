using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEnvironmentHandler : MonoBehaviour
{
    public GameObject[] _propPrefabs = null;
    public GameObject _grid = null;

    public InteractiveEnvironmentHandler _interactiveEnvironment = null;
    public ExtraEnvironmentBehaviour _extraBehaviour = null;

    public string _extraEnvironmentAmbienceSound = "";

    /*
    bool setAmbience = false;
    [FMODUnity.EventRef, SerializeField] string _AmbienceSoundSetter = "";
    private void OnValidate()
    {
        if(!setAmbience)
            return;
        setAmbience = false;
        _extraEnvironmentAmbienceSound = _AmbienceSoundSetter;
        UnityEditor.EditorUtility.SetDirty(this);
    }
    */

    #region PROPERTIES
    public GameObject[] PropPrefabs { get => _propPrefabs; }
    public GameObject Grid { get => _grid; }
    public bool HasExtraAmbience { get => _extraEnvironmentAmbienceSound != ""; }
    public string ExtraAmbienceSound { get => _extraEnvironmentAmbienceSound; }
    #endregion

    private void Awake()
    {
        /*
        if (Tools.DebugUtils.videoMode)
            Grid?.SetActive(false);
        */

        if (_propPrefabs == null || _propPrefabs.Length <= 0)
            return;
        int randomNum = Random.Range(0, _propPrefabs.Length);
        Instantiate(_propPrefabs[randomNum], transform);
    }

    #region Interactive Environments
    public void SetUpNotifications()
    {
        if (_interactiveEnvironment != null)
            _interactiveEnvironment.SetUpNotifications();
    }
    public void InitializeExtraData(IGameCheckData gameCheckDB)
    {
        _extraBehaviour?.SetUpExtraEnvironment(gameCheckDB);
    }
    public void TriggerInteractiveEnvironment(IExtraCombatAmbienceOptions audioOptions, int id)
    {
        if (_interactiveEnvironment != null)
            _interactiveEnvironment.TriggerEnvironment(audioOptions, id);
    }
    #endregion
}
