using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDialogueByQuest_RoomItemModData : BaseRoomItemModData
{
    public string m_CustomDialogueID = "";
    public string m_QuestActivatorID = "";
    public override string RoomItemType => RoomItemType_GameIDs.CustomDialogue.ToString();
}
