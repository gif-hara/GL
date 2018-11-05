using GL.Database;
using UnityEngine;

namespace GL.Battle.Commands
{
    /// <summary>
    /// 同じタイプの武器を装備しているか
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Condition/SameTypeEquipment")]
    public sealed class SameTypeEquipment : EquipmentElementCondition
    {
        public override bool Suitable(EquipmentRecord rightWeapon, EquipmentRecord leftWeapon, EquipmentRecord[] accessories)
        {
            if(rightWeapon == null || leftWeapon == null)
            {
                return false;
            }
            
            return rightWeapon.EquipmentType == leftWeapon.EquipmentType;
        }
    }
}
