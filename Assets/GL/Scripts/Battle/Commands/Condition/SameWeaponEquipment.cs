using GL.Database;
using UnityEngine;

namespace GL.Battle.Commands
{
    /// <summary>
    /// 同じ武器を装備しているか
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Condition/SameWeaponEquipment")]
    public sealed class SameWeaponEquipment : EquipmentElementCondition
    {
        public override bool Suitable(EquipmentRecord rightWeapon, EquipmentRecord leftWeapon, EquipmentRecord[] accessories)
        {
            if(rightWeapon == null || leftWeapon == null)
            {
                return false;
            }
            
            return rightWeapon.Id == leftWeapon.Id;
        }
    }
}
