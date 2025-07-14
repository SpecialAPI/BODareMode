using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDialogue_RoomItemModData : BaseRoomItemModData
{
    public string _CustomDialogueID = "";
    public override string RoomItemType => RoomItemType_GameIDs.CustomDialogue.ToString();
}
