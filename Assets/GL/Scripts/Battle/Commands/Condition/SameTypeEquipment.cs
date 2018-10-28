using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.Commands
{
    /// <summary>
    /// 同じタイプの武器を装備しているか
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Condition/SameTypeEquipment")]
    public sealed class SameTypeEquipment : CommandElementCondition
    {
        public override bool Suitable(User.Player player)
        {
            return player.LeftHand.BattleWeapon.WeaponType == player.RightHand.BattleWeapon.WeaponType;
        }
    }
}
