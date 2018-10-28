using GL.Database;
using UnityEngine;

namespace GL.Battle.Commands
{
    /// <summary>
    /// 同じタイプの武器を装備しているか
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Condition/SameTypeEquipment")]
    public sealed class SameTypeEquipment : CommandElementCondition
    {
        public override bool Suitable(WeaponRecord rightWeapon, WeaponRecord leftWeapon, AccessoryRecord[] accessories)
        {
            if(rightWeapon == null || leftWeapon == null)
            {
                return false;
            }
            
            return rightWeapon.WeaponType == leftWeapon.WeaponType;
        }
    }
}
