using System.Linq;
using UnityEngine;

namespace GL.Database
{
    /// <summary>
    /// 武器データベース
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/List/Weapon")]
    public sealed class WeaponList : MasterDataRecordList<WeaponRecord>
    {
        protected override string FindAssetsFilter => "t:Weapon";

        protected override string[] FindAssetsPaths => new[] { "Assets/GL/MasterData/Weapons" };
    }
}
