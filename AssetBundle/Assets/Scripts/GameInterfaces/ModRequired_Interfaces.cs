using System.Collections.Generic;
#region Game Data
public interface IGameCheckData
{
    int GetIntData(string variableName);
    bool GetBoolData(string variableName);
    bool DidCompleteQuest(string questName);
    bool IsItemUnlocked(string itemName);
    HashSet<string> GetUnlockedCharacters();
    EnemyKilledSaveData GetEnemyKilledData(string enemyName);
}
#endregion

#region Combat Data
public interface IExtraCombatAmbienceOptions
{
    void TryStopAmbience();
    void StartExtraCombatAmbienceEvent(string extraAmbience);
    void TrySwapExtraCombatAmbienceEvent(string extraCombatName);
}
#endregion


