using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Database
{
    /// <summary>
    /// 素材データベース
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/Material")]
    public sealed class Material : DatabaseList<Materials.Material>
    {
        protected override string FindAssetsFilter => "l:GK.Material";

        protected override string[] FindAssetsPaths => new[] { "Assets/GL/MasterData/Materials" };
    }
}
