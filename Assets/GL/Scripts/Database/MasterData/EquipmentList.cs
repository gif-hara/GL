using System.Linq;
using UnityEngine;

namespace GL.Database
{
    /// <summary>
    /// 装備品データベース
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/List/Equipment")]
    public sealed class EquipmentList : MasterDataRecordList<EquipmentRecord>
    {
        protected override string FindAssetsFilter => "t:EquipmentRecord";

        protected override string[] FindAssetsPaths => new[] { "Assets/GL/MasterData/Equipments" };
    }
}
