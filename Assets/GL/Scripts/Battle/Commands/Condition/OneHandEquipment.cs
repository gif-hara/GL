using GL.Database;
using UnityEngine;

namespace GL.Battle.Commands
{
    /// <summary>
    /// 片手のみ武器を装備しているか
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Condition/OneHandEquipment")]
    public sealed class OneHandEquipment : EquipmentElementCondition
    {
        public override bool Suitable(EquipmentRecord rightWeapon, EquipmentRecord leftWeapon, EquipmentRecord[] accessories)
        {
            return
                rightWeapon == null && leftWeapon != null ||
                rightWeapon != null && leftWeapon == null;
        }
    }
}
