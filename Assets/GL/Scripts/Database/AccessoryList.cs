using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Database
{
    /// <summary>
    /// アクセサリーデータベース
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/Accessory")]
    public sealed class AccessoryList : MasterDataList<Battle.Accessory>
    {
        protected override string FindAssetsFilter => "t:Accessory";

        protected override string[] FindAssetsPaths => new[] { "Assets/GL/MasterData/Accessories" };
    }
}
