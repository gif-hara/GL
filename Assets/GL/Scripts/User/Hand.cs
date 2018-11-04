using System;
using GL.Database;
using UnityEngine;

namespace GL.User
{
    /// <summary>
    /// 手
    /// </summary>
    /// <remarks>
    /// 武器を持つことが出来る
    /// </remarks>
    [Serializable]
    public sealed class Hand
    {
        [SerializeField]
        private int equipmentInstanceId;
        public int EquipmentInstanceId { get { return this.equipmentInstanceId; } set { this.equipmentInstanceId = value; } }

        /// <summary>
        /// 武器を所持しているか返す
        /// </summary>
        public bool IsPossession => this.equipmentInstanceId != 0;

        public User.Equipment UserWeapon => UserData.Instance.Weapons.GetByInstanceId(this.equipmentInstanceId);

        public EquipmentRecord EquipmentRecord => this.IsPossession ? this.UserWeapon.EquipmentRecord : null;

        public Hand(int equipmentInstanceId)
        {
            this.equipmentInstanceId = equipmentInstanceId;
        }
    }
}
