using HK.GL.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Database
{
    /// <summary>
    /// 素材データベース
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/List/Material")]
    public sealed class MaterialList : MasterDataRecordList<MaterialRecord>
    {
        protected override string FindAssetsFilter => "l:GL.Material";

        protected override string[] FindAssetsPaths => new[] { "Assets/GL/MasterData/Materials" };

#if UNITY_EDITOR
        public MaterialRecord GetFromName(string materialName)
        {
            var result = this.List.Find(m => m.MaterialName == materialName);
            Assert.IsNotNull(result, $"{materialName}の素材の取得に失敗しました");
            return result;
        }
#endif
    }
}
