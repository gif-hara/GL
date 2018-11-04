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
    public sealed class EquipmentImporter
    {
        [MenuItem("GL/MasterData/Import Equipment")]
        private static void Import()
        {
            var equipmentRecordData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - EquipmentRecord.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var equipmentCommandData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - EquipmentCommand.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var equipmentSkillData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - EquipmentSkill.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var equipmentNeedMaterialData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - EquipmentNeedMaterial.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            // コマンドデータを抽出
            var commandDictionary = new Dictionary<string, List<ConditionalCommandRecord>>();
            for (var i = 1; i < equipmentCommandData.Length; i++)
            {
                var splitCommandData = equipmentCommandData[i].Split(',');
                var conditionName = splitCommandData[2];
                var equipmentId = splitCommandData[3];
                var commandId = splitCommandData[4].RemoveNewLine();
                List<ConditionalCommandRecord> commandRecords = null;
                if(!commandDictionary.TryGetValue(equipmentId, out commandRecords))
                {
                    commandRecords = new List<ConditionalCommandRecord>();
                    commandDictionary.Add(equipmentId, commandRecords);
                }
                var commandRecord = AssetDatabase.LoadAssetAtPath<CommandRecord>($"Assets/GL/MasterData/Commands/Bundles/{commandId}.asset");
                Assert.IsNotNull(commandRecord, $"{commandId}のCommandRecordがありませんでした");
                var condition = AssetDatabase.LoadAssetAtPath<CommandElementCondition>($"Assets/GL/MasterData/Commands/Conditions/{conditionName}.asset");
                Assert.IsNotNull(condition, $"{conditionName}に対応するCommandElementConditionがありませんでした");

                commandRecords.Add(new ConditionalCommandRecord(commandRecord, condition));
            }

            // スキルデータを抽出
            var skillElementDictionary = new Dictionary<string, List<SkillElement>>();
            for (var i = 1; i < equipmentSkillData.Length; i++)
            {
                var splitSkillData = equipmentSkillData[i].Split(',');
                var equipmentId = splitSkillData[2];
                var skillId = splitSkillData[3].RemoveNewLine();
                List<SkillElement> skillElements = null;
                if(!skillElementDictionary.TryGetValue(equipmentId, out skillElements))
                {
                    skillElements = new List<SkillElement>();
                    skillElementDictionary.Add(equipmentId, skillElements);
                }

                var skillElement = AssetDatabase.LoadAssetAtPath<SkillElement>($"Assets/GL/MasterData/SkillElements/{skillId}.asset");
                Assert.IsNotNull(skillElement, $"{skillId}に対応する{typeof(SkillElement).Name}がありませんでした");
                skillElements.Add(skillElement);
            }

            // 必要素材を抽出
            var needMaterialDictionary = new Dictionary<string, List<NeedMaterial>>();
            var materialData = AssetDatabase.LoadAssetAtPath<MaterialList>("Assets/GL/MasterData/Database/Material.asset");
            Assert.IsNotNull(materialData, "MaterialListの取得に失敗しました");
            for (var i = 1; i < equipmentNeedMaterialData.Length; i++)
            {
                var splitNeedMaterialData = equipmentNeedMaterialData[i].Split(',');
                var materialName = splitNeedMaterialData[1];
                var equipmentId = splitNeedMaterialData[3].RemoveNewLine();
                List<NeedMaterial> needMaterials = null;
                if(!needMaterialDictionary.TryGetValue(equipmentId, out needMaterials))
                {
                    needMaterials = new List<NeedMaterial>();
                    needMaterialDictionary.Add(equipmentId, needMaterials);
                }

                var material = materialData.GetFromName(materialName);
                needMaterials.Add(new NeedMaterial(material, int.Parse(splitNeedMaterialData[2])));
            }

            // EquipmentRecordを作成
            var equipmentNameAsset = AssetDatabase.LoadAssetAtPath<StringAsset>("Assets/GL/StringAssets/EquipmentName.asset");
            for (var i = 1; i < equipmentRecordData.Length; i++)
            {
                var splitEquipmentRecordData = equipmentRecordData[i].Split(',');
                var path = "Assets/GL/MasterData/Equipments/";
                var fileName = splitEquipmentRecordData[0];
                var equipmentRecord = ImporterUtility.GetOrCreate<EquipmentRecord>(path, fileName);
                List<ConditionalCommandRecord> commandRecords = null;
                commandDictionary.TryGetValue(fileName, out commandRecords);
                List<SkillElement> skillElements = null;
                skillElementDictionary.TryGetValue(fileName, out skillElements);
                List<NeedMaterial> needMaterials = null;
                needMaterialDictionary.TryGetValue(fileName, out needMaterials);
                equipmentRecord.Set(
                    equipmentNameAsset.CreateOrGetFinder(splitEquipmentRecordData[1]),
                    int.Parse(splitEquipmentRecordData[2]),
                    (Constants.EquipmentType)Enum.Parse(typeof(Constants.EquipmentType), splitEquipmentRecordData[3]),
                    int.Parse(splitEquipmentRecordData[4]),
                    commandRecords == null ? null : commandRecords.ToArray(),
                    skillElements == null ? null : skillElements.ToArray(),
                    needMaterials == null ? null : needMaterials.ToArray()
                );
                EditorUtility.SetDirty(equipmentRecord);
            }

            var database = AssetDatabase.LoadAssetAtPath<EquipmentList>("Assets/GL/MasterData/Database/Equipment.asset");
            database.Reset();
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }
    }
}
#endif

