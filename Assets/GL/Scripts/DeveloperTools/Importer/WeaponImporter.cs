using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections.Generic;
using GL.Database;
using GL.Extensions;
using GL.Battle.Commands;
using HK.Framework.Text;

#if UNITY_EDITOR
using UnityEditor;

namespace GL.DeveloperTools
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class WeaponImporter
    {
        [MenuItem("GL/MasterData/Import Weapon")]
        private static void Import()
        {
            var weaponRecordData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - WeaponRecord.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var weaponCommandData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - WeaponCommand.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var weaponSkillData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - WeaponSkill.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var weaponNeedMaterialData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - WeaponNeedMaterial.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            // コマンドデータを抽出
            var commandDictionary = new Dictionary<string, List<ConditionalCommandRecord>>();
            for (var i = 1; i < weaponCommandData.Length; i++)
            {
                var splitCommandData = weaponCommandData[i].Split(',');
                var conditionName = splitCommandData[2];
                var weaponId = splitCommandData[3];
                var commandId = splitCommandData[4].RemoveNewLine();
                List<ConditionalCommandRecord> commandRecords = null;
                if(!commandDictionary.TryGetValue(weaponId, out commandRecords))
                {
                    commandRecords = new List<ConditionalCommandRecord>();
                    commandDictionary.Add(weaponId, commandRecords);
                }
                var commandRecord = AssetDatabase.LoadAssetAtPath<CommandRecord>($"Assets/GL/MasterData/Commands/Bundles/{commandId}.asset");
                Assert.IsNotNull(commandRecord, $"{commandId}のCommandRecordがありませんでした");
                var condition = AssetDatabase.LoadAssetAtPath<CommandElementCondition>($"Assets/GL/MasterData/Commands/Conditions/{conditionName}.asset");
                Assert.IsNotNull(condition, $"{conditionName}に対応するCommandElementConditionがありませんでした");

                commandRecords.Add(new ConditionalCommandRecord(commandRecord, condition));
            }

            // スキルデータを抽出
            var skillElementDictionary = new Dictionary<string, List<SkillElement>>();
            for (var i = 1; i < weaponSkillData.Length; i++)
            {
                var splitSkillData = weaponSkillData[i].Split(',');
                var weaponId = splitSkillData[2];
                var skillId = splitSkillData[3].RemoveNewLine();
                List<SkillElement> skillElements = null;
                if(!skillElementDictionary.TryGetValue(weaponId, out skillElements))
                {
                    skillElements = new List<SkillElement>();
                    skillElementDictionary.Add(weaponId, skillElements);
                }

                var skillElement = AssetDatabase.LoadAssetAtPath<SkillElement>($"Assets/GL/MasterData/SkillElements/{skillId}.asset");
                Assert.IsNotNull(skillElement, $"{skillId}に対応する{typeof(SkillElement).Name}がありませんでした");
                skillElements.Add(skillElement);
            }

            // 必要素材を抽出
            var needMaterialDictionary = new Dictionary<string, List<NeedMaterial>>();
            var materialData = AssetDatabase.LoadAssetAtPath<MaterialList>("Assets/GL/MasterData/Database/Material.asset");
            Assert.IsNotNull(materialData, "MaterialListの取得に失敗しました");
            for (var i = 1; i < weaponNeedMaterialData.Length; i++)
            {
                var splitNeedMaterialData = weaponNeedMaterialData[i].Split(',');
                var materialName = splitNeedMaterialData[1];
                var weaponId = splitNeedMaterialData[3].RemoveNewLine();
                List<NeedMaterial> needMaterials = null;
                if(!needMaterialDictionary.TryGetValue(weaponId, out needMaterials))
                {
                    needMaterials = new List<NeedMaterial>();
                    needMaterialDictionary.Add(weaponId, needMaterials);
                }

                var material = materialData.GetFromName(materialName);
                needMaterials.Add(new NeedMaterial(material, int.Parse(splitNeedMaterialData[2])));
            }

            // WeaponRecordを作成
            var weaponNameAsset = AssetDatabase.LoadAssetAtPath<StringAsset>("Assets/GL/StringAssets/WeaponName.asset");
            for (var i = 1; i < weaponRecordData.Length; i++)
            {
                var splitWeaponRecordData = weaponRecordData[i].Split(',');
                var path = "Assets/GL/MasterData/Weapons/";
                var fileName = splitWeaponRecordData[0];
                var weaponRecord = ImporterUtility.GetOrCreate<EquipmentRecord>(path, fileName);
                List<SkillElement> skillElements = null;
                skillElementDictionary.TryGetValue(fileName, out skillElements);
                List<NeedMaterial> needMaterials = null;
                needMaterialDictionary.TryGetValue(fileName, out needMaterials);
                weaponRecord.Set(
                    weaponNameAsset.CreateOrGetFinder(splitWeaponRecordData[1]),
                    int.Parse(splitWeaponRecordData[2]),
                    (Constants.EquipmentType)Enum.Parse(typeof(Constants.EquipmentType), splitWeaponRecordData[3]),
                    int.Parse(splitWeaponRecordData[4]),
                    commandDictionary[fileName].ToArray(),
                    skillElements == null ? null : skillElements.ToArray(),
                    needMaterials == null ? null : needMaterials.ToArray()
                );
                EditorUtility.SetDirty(weaponRecord);
            }

            var database = AssetDatabase.LoadAssetAtPath<EquipmentList>("Assets/GL/MasterData/Database/Weapon.asset");
            database.Reset();
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }
    }
}
#endif

