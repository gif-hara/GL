using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using HK.GL.Extensions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GL.Database
{
    /// <summary>
    /// レコードを保持する抽象クラス
    /// </summary>
    public abstract class MasterDataRecordList<T> : ScriptableObject, IMasterDataRecordList<T> where T : ScriptableObject, IMasterDataRecord
    {
        [SerializeField]
        protected T[] list;
        public T[] List => this.list;

        public T GetById(string id) => this.list.Find(t => t.Id == id);

        protected abstract string FindAssetsFilter { get; }

        protected abstract string[] FindAssetsPaths { get; }

#if UNITY_EDITOR
        public void Reset()
        {
            this.list = AssetDatabase.FindAssets(this.FindAssetsFilter, this.FindAssetsPaths)
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>)
                .ToArray();
        }

        public void ResetAndDirty()
        {
            this.list = AssetDatabase.FindAssets(this.FindAssetsFilter, this.FindAssetsPaths)
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>)
                .ToArray();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}
