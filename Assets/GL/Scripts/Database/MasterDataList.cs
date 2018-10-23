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
    /// リスト形式で保持するマスターデータの抽象クラス
    /// </summary>
    public abstract class MasterDataList<T> : ScriptableObject, IMasterDataList<T> where T : ScriptableObject, IMasterDataRecord
    {
        [SerializeField]
        protected T[] list;
        public T[] List => this.list;

        public T GetById(string id) => this.list.Find(t => t.Id == id);

        protected abstract string FindAssetsFilter { get; }

        protected abstract string[] FindAssetsPaths { get; }

#if UNITY_EDITOR
        void Reset()
        {
            this.list = AssetDatabase.FindAssets(this.FindAssetsFilter, this.FindAssetsPaths)
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>)
                .ToArray();
        }
#endif
    }
}
