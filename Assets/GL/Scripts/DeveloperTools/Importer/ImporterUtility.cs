using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.DeveloperTools
{
    /// <summary>
    /// 
    /// </summary>
    public static class ImporterUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">ファイル名を含めないフォルダのパス</param>
        /// <param name="fileName">ファイル名</param>
        /// <param name="extension">拡張子</param>
        public static T GetOrCreate<T>(string path, string fileName, string extension = ".asset") where T : ScriptableObject
        {
            path = $"{path}{fileName}{extension}";
            if (File.Exists(path))
            {
                return AssetDatabase.LoadAssetAtPath<T>(path);
            }
            else
            {
                var result = ScriptableObject.CreateInstance<T>();
                result.name = fileName;
                AssetDatabase.CreateAsset(result, path);

                return result;
            }
        }
    }
}
