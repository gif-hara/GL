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
        private int weaponInstanceId;
        public int WeaponInstanceId { get { return this.weaponInstanceId; } set { this.weaponInstanceId = value; } }

        /// <summary>
        /// 武器を所持しているか返す
        /// </summary>
        public bool IsPossession => this.weaponInstanceId != 0;

        public User.Weapon UserWeapon => UserData.Instance.Weapons.GetByInstanceId(this.weaponInstanceId);

        public WeaponRecord BattleWeapon => this.IsPossession ? this.UserWeapon.BattleWeapon : null;

        public Hand(int weaponInstanceId)
        {
            this.weaponInstanceId = weaponInstanceId;
        }
    }
}
