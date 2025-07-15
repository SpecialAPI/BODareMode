using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BODareMode.Serializer
{
    public static class ScriptableObjectSerializer
    {
        public static Dictionary<Type, Delegate> Deserializers = new()
        {
            // Basegame types
            [typeof(CharacterSO)] = LoadedAssetsHandler.GetCharacter,
            [typeof(ConditionEncounterSO)] = LoadedAssetsHandler.GetConditionEncounter,
            [typeof(FreeFoolEncounterSO)] = LoadedAssetsHandler.GetFreeFoolEncounter,
            [typeof(BasicEncounterSO)] = LoadedAssetsHandler.GetBasicEncounter,
            [typeof(EnemySO)] = LoadedAssetsHandler.GetEnemy,
            [typeof(BaseBundleGeneratorSO)] = LoadedAssetsHandler.GetEnemyBundle,
            //[typeof(EnemyEncounterSelectorSO)] = , LoadedAssetsHandler.LoadedEES exists but it's never used for whatever reason. spaghimon.
            [typeof(BaseWearableSO)] = LoadedAssetsHandler.GetWearable,
            //[typeof(AbilitySO)] = , for some reason abilities are split into two getter functions making my life a nightmare
            [typeof(BasePassiveAbilitySO)] = LoadedAssetsHandler.GetPassive,
            [typeof(SpeakerData)] = LoadedAssetsHandler.GetSpeakerData,
            [typeof(DialogueSO)] = LoadedAssetsHandler.GetDialogueData,
            [typeof(ZoneDataBaseSO)] = LoadedAssetsHandler.GetZoneDB,

            // Custom types
            [typeof(DareSO)] = DareDatabase.GetDare
        };

        public static bool CanSerializeType(Type type)
        {
            if(!type.IsSubclassOf(typeof(ScriptableObject)))
                return false;

            for(var t = type; t != null && t != typeof(ScriptableObject); t = t.BaseType)
            {
                if (Deserializers.ContainsKey(t))
                    return true;
            }

            return false;
        }

        public static bool TryDeserializeScriptable(Type type, string id, out object res)
        {
            res = null;

            if (!type.IsSubclassOf(typeof(ScriptableObject)))
                return false;

            for (var t = type; t != null && t != typeof(ScriptableObject); t = t.BaseType)
            {
                if (!Deserializers.ContainsKey(t))
                    continue;

                res = Deserializers[t]?.DynamicInvoke(id);
                return true;
            }

            return false;
        }
    }
}
