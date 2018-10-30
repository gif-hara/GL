using System;
using System.IO;
using GL.Database;
using HK.Framework.Text;
using HK.GL.Extensions;
using UnityEngine;
using UnityEngine.Assertions;
using GL.Extensions;

#if UNITY_EDITOR
using UnityEditor;
namespace GL.DeveloperTools
{
    /// <summary>
    /// 
    /// </summary>
    public static class MaterialImporter
    {
        [MenuItem("GL/MasterData/Import Material")]
        private static void Import()
        {
            var splitData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - Material.csv");
            var data = splitData.text.Split(new string[]{ System.Environment.NewLine }, StringSplitOptions.None);
            var materialNameAsset = AssetDatabase.LoadAssetAtPath<StringAsset>("Assets/GL/StringAssets/MaterialName.asset");

            var typeId = 1;
            var index = 1;
            data.ForEach(d =>
            {
                var split = d.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                // 空白の場合はID類をリセットする
                if(string.IsNullOrWhiteSpace(split[0]))
                {
                    typeId++;
                    index = 1;
                    return;
                }

                var materialName = d.RemoveNewLine();
                var fileName = $"1{typeId:00}{index:00}";
                index++;

                var path = "Assets/GL/MasterData/Materials/";
                var materialAsset = ImporterUtility.GetOrCreate<MaterialRecord>(path, fileName);

                AssetDatabase.SetLabels(materialAsset, new string[] { "GL.Material" });
                var materialNameFinder = materialNameAsset.CreateOrGetFinder(materialName);
                materialAsset.Set(materialNameFinder);
                EditorUtility.SetDirty(materialAsset);
            });

            var materialDatabase = AssetDatabase.LoadAssetAtPath<MaterialList>("Assets/GL/MasterData/Database/Material.asset");
            materialDatabase.Reset();
            AssetDatabase.SaveAssets();
        }
    }
}
#endif
