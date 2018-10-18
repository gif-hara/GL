using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GL.MasterData
{
    /// <summary>
    /// リスト形式で保持するデータベースの抽象クラス
    /// </summary>
    public abstract class DatabaseList<T> : ScriptableObject, IDataBaseList<T> where T : ScriptableObject
    {
        [SerializeField]
        protected T[] list;
        public T[] List { get { return this.list; } }

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
