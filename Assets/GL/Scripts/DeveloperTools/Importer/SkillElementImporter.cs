using UnityEngine;
using UnityEngine.Assertions;
using System;
using GL.Database;
using HK.Framework.Text;

#if UNITY_EDITOR

using UnityEditor;

namespace GL.DeveloperTools
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SkillElementImporter
    {
        [MenuItem("GL/MasterData/Import SkillElement")]
        public static void Import()
        {
            var skillElementData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - SkillElement.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var elementNameAsset = AssetDatabase.LoadAssetAtPath<StringAsset>("Assets/GL/StringAssets/SkillElementName.asset");
            var descriptionAsset = AssetDatabase.LoadAssetAtPath<StringAsset>("Assets/GL/StringAssets/SkillElementDescription.asset");
            for (var i = 1; i < skillElementData.Length; i++)
            {
                var splitSkillElementData = skillElementData[i].Split(',');
                var skillElement = GetOrCreate(splitSkillElementData, elementNameAsset, descriptionAsset);
                EditorUtility.SetDirty(skillElement);
            }

            AssetDatabase.SaveAssets();
        }

        private static SkillElement GetOrCreate(string[] splitSkillElementData, StringAsset elementNameAsset, StringAsset descriptionAsset)
        {
            var path = "Assets/GL/MasterData/SkillElements/";
            var fileName = splitSkillElementData[0];
            var type = splitSkillElementData[3];

            switch(type)
            {
                case "AddStatusParameterFixed":
                    return ImporterUtility.GetOrCreate<AddStatusParameterFixed>(path, fileName).Setup(
                        elementNameAsset.CreateOrGetFinder(splitSkillElementData[1]),
                        descriptionAsset.CreateOrGetFinder(splitSkillElementData[2]),
                        (Constants.StatusParameterType)Enum.Parse(typeof(Constants.StatusParameterType), splitSkillElementData[4]),
                        int.Parse(splitSkillElementData[5])
                    );
                case "AddStatusParameterRate":
                    return ImporterUtility.GetOrCreate<AddStatusParameterRate>(path, fileName).Setup(
                        elementNameAsset.CreateOrGetFinder(splitSkillElementData[1]),
                        descriptionAsset.CreateOrGetFinder(splitSkillElementData[2]),
                        (Constants.StatusParameterType)Enum.Parse(typeof(Constants.StatusParameterType), splitSkillElementData[4]),
                        float.Parse(splitSkillElementData[5])
                    );
                case "AddStatusResistance":
                    return ImporterUtility.GetOrCreate<AddStatusResistance>(path, fileName).Setup(
                        elementNameAsset.CreateOrGetFinder(splitSkillElementData[1]),
                        descriptionAsset.CreateOrGetFinder(splitSkillElementData[2]),
                        (Constants.StatusAilmentType)Enum.Parse(typeof(Constants.StatusAilmentType), splitSkillElementData[4]),
                        float.Parse(splitSkillElementData[5])
                    );
                default:
                    Assert.IsTrue(false, $"{type}は未対応の値です");
                    return null;
            }
        }
    }
}

#endif
