using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections.Generic;
using GL.User;
using GL.Extensions;
using GL.Database;
using HK.Framework.Text;

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
                var parameter = new Battle.PartyControllers.Parameter();
                // parameter.Set(
                //     1,

                // )
                // partyRecord.Set(
                //     partyNameAsset.CreateOrGetFinder(splitPartyData[1]),

                // )
            }
        }
    }
}

#endif
