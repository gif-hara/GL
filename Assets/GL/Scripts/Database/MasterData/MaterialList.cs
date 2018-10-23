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
        protected override string FindAssetsFilter => "l:GK.Material";

        protected override string[] FindAssetsPaths => new[] { "Assets/GL/MasterData/Materials" };
    }
}
