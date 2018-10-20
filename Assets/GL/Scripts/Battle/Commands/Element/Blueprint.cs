using UnityEditor;
using UnityEngine;

namespace GL.Battle.Commands.Element
{
    /// <summary>
    /// コマンド1個単位の挙動の設計図の抽象クラス
    /// </summary>
    public abstract class Blueprint : ScriptableObject
    {
        public abstract IImplement Create();

#if UNITY_EDITOR
        public abstract string FileName { get; }

        // 重い
        // void OnValidate()
        // {
        //     this.Rename();
        // }

        [ContextMenu("Rename")]
        void Rename()
        {
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(this), this.FileName);
        }
#endif
    }
}
