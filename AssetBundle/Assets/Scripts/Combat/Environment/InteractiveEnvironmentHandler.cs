using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveEnvironmentHandler : MonoBehaviour
{
    public abstract void SetUpNotifications();
    public abstract void TriggerEnvironment(IExtraCombatAmbienceOptions audioOptions, int id);
}
