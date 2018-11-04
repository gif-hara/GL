using GL.Database;
using UnityEngine;

namespace GL.Battle.Commands
{
    /// <summary>
    /// 無条件でコマンド実行出来る
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Condition/Unconditional")]
    public sealed class Unconditional : CommandElementCondition
    {
        public override bool Suitable(EquipmentRecord rightWeapon, EquipmentRecord leftWeapon, EquipmentRecord[] accessories)
        {
            return true;
        }
    }
}
