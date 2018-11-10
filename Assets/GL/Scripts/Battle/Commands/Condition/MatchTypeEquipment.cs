using GL.Database;
using UnityEngine;

namespace GL.Battle.Commands
{
    /// <summary>
    /// 同じタイプの武器を装備しているか
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/Condition/MatchTypeEquipment")]
    public sealed class MatchTypeEquipment : EquipmentElementCondition
    {
        [SerializeField]
        private Constants.EquipmentType target;

        public override string Description => this.description.Format(MasterData.ConstantString.Equipment.Get(this.target));

        public override bool Suitable(EquipmentRecord rightWeapon, EquipmentRecord leftWeapon, EquipmentRecord[] accessories)
        {
            return
                rightWeapon != null && rightWeapon.EquipmentType == this.target ||
                leftWeapon != null && leftWeapon.EquipmentType == this.target;
        }
    }
}
