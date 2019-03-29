using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections.Generic;
using GL.User;
using GL.Extensions;
using GL.Database;
using HK.Framework.Text;
using GL.Battle;

#if UNITY_EDITOR
using UnityEditor;

namespace GL.DeveloperTools
{
    /// <summary>
    /// 
    /// </summary>
    public static class PartyImporter
    {
        [MenuItem("GL/MasterData/Import Party")]
        private static void Import()
        {
            var partyData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - Party.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var partyUnlockData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - PartyUnlock.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var unlockDictionary = new Dictionary<string, UnlockElements>();
            for (var i = 1; i < partyUnlockData.Length; i++)
            {
                var splitPartyUnlockData = partyUnlockData[i].Split(',');
                var partyId = splitPartyUnlockData[3].RemoveNewLine();
                UnlockElements unlockElements = null;
                if(!unlockDictionary.TryGetValue(partyId, out unlockElements))
                {
                    unlockElements = new UnlockElements();
                    unlockDictionary.Add(partyId, unlockElements);
                }

                var type = splitPartyUnlockData[1];
                var id = splitPartyUnlockData[2];
                switch(type)
                {
                    case "EnemyParty":
                        unlockElements.EnemyParties.Add(id);
                        break;
                    case "Character":
                        unlockElements.Characters.Add(id);
                        break;
                    case "Equipment":
                        unlockElements.Equipments.Add(id);
                        break;
                    default:
                        Assert.IsTrue(false, $"{type}は未対応の値です");
                        break;
                }
            }

            var partyNameAsset = AssetDatabase.LoadAssetAtPath<StringAsset>("Assets/GL/StringAssets/PartyName.asset");
            for (var i = 1; i < partyData.Length; i++)
            {
                var splitPartyData = partyData[i].Split(',');
                var path = "Assets/GL/MasterData/Parties/Enemy/";
                var fileName = splitPartyData[0];
                var partyRecord = ImporterUtility.GetOrCreate<PartyRecord>(path, fileName);
                var parameters = CreateParameters(splitPartyData);
                UnlockElements unlockElements = null;
                unlockDictionary.TryGetValue(fileName, out unlockElements);
                partyRecord.Set(
                    partyNameAsset.CreateOrGetFinder(splitPartyData[1]),
                    parameters,
                    Constants.CharacterType.Enemy,
                    unlockElements
                );
                AssetDatabase.SetLabels(partyRecord, new string[] { "GL.PartyRecord" });
                EditorUtility.SetDirty(partyRecord);
            }

            var partyDatabase = AssetDatabase.LoadAssetAtPath<EnemyPartyList>("Assets/GL/MasterData/Database/EnemyParty.asset");
            partyDatabase.Reset();
            EditorUtility.SetDirty(partyDatabase);

            AssetDatabase.SaveAssets();
        }

        private static PartyParameter[] CreateParameters(string[] splitPartyData)
        {
            var result = new List<PartyParameter>();
            for (var i = 2; i <= 9; i++)
            {
                var characterId = splitPartyData[i];
                if(string.IsNullOrEmpty(characterId))
                {
                    continue;
                }

                var characterRecord = AssetDatabase.LoadAssetAtPath<CharacterRecord>($"Assets/GL/MasterData/Characters/Enemy/{characterId}.asset");
                Assert.IsNotNull(characterRecord, $"{characterId}に対応する{typeof(CharacterRecord).Name}の取得に失敗しました");
                var weaponRecord = AssetDatabase.LoadAssetAtPath<EquipmentRecord>($"Assets/GL/MasterData/Equipments/{characterId}.asset");
                Assert.IsNotNull(characterRecord, $"{characterId}に対応する武器の取得に失敗しました");

                var parameter = new PartyParameter();
                parameter.Set(
                    1,
                    characterRecord,
                    weaponRecord,
                    null,
                    null
                    );

                result.Add(parameter);
            }

            return result.ToArray();
        }
    }
}

#endif
