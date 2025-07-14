using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRoomHandlerModData : MonoBehaviour
{
    public abstract string RoomHandlerType { get; }

}

public enum RoomHandlerType_GameIDs
{
    NPC,
    Fools,
    Shop,
    Enemy,
    Boss,
    MoneyChest,
    Treasure
}