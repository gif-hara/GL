using System;
using System.IO;
using GL.Database;
using HK.Framework.Text;
using HK.GL.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

#if UNITY_EDITOR
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
                var split = d.Split(',');
                
                // 空白の場合はID類をリセットする
                if(string.IsNullOrWhiteSpace(split[0]))
                {
                    typeId++;
                    index = 1;
                    return;
                }

                var materialName = split[0];
                var fileName = $"1{typeId:00}{index:00}";
                var extension = ".asset";
                index++;


                var path = $"Assets/GL/MasterData/Materials/{fileName}{extension}";
                MaterialRecord materialAsset;
                if(File.Exists(path))
                {
                    materialAsset = AssetDatabase.LoadAssetAtPath<MaterialRecord>(path);
                }
                else
                {
                    materialAsset = ScriptableObject.CreateInstance<MaterialRecord>();
                    materialAsset.name = fileName;
                    AssetDatabase.CreateAsset(materialAsset, path);
                    AssetDatabase.SetLabels(materialAsset, new string[] { "GL.Material" });
                }

                var materialNameFinder = materialNameAsset.CreateOrGetFinder(materialName);
                materialAsset.Set(materialNameFinder);
            });

            var materialDatabase = AssetDatabase.LoadAssetAtPath<MaterialList>("Assets/GL/MasterData/Database/Material.asset");
            materialDatabase.Reset();
            AssetDatabase.SaveAssets();
        }
    }
}
#endif
