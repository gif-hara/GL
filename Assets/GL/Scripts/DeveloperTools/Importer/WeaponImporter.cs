using UnityEngine;
using UnityEngine.Assertions;
using System;

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
            var weaponNeedMaterialData = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/GL/MasterData/RawData/GL - WeaponNeedMaterial.csv")
                .text
                .Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
#endif

