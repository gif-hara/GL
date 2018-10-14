using UnityEngine;
using UnityEngine.Assertions;

namespace GL.MasterData
{
    /// <summary>
    /// アクセサリーデータベース
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/Accessory")]
    public sealed class Accessory : DatabaseList<Battle.Accessories.Accessory>
    {
        protected override string FindAssetsFilter => "t:Accessory";

        protected override string[] FindAssetsPaths => new[] { "Assets/GL/MasterData/Accessories" };
    }
}
