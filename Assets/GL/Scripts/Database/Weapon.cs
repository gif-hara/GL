using System.Linq;
using UnityEngine;

namespace GL.Database
{
    /// <summary>
    /// 武器データベース
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/Weapon")]
    public sealed class Weapon : DatabaseList<Battle.Weapon>
    {
        protected override string FindAssetsFilter => "t:Weapon";

        protected override string[] FindAssetsPaths => new[] { "Assets/GL/MasterData/Weapons" };
    }
}
