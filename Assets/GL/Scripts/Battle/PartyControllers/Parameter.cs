using System;
using System.Linq;
using GL.Battle;
using GL.MasterData;
using GL.User;
using HK.GL.Extensions;
using UnityEngine;

namespace GL.Battle.PartyControllers
{
    /// <summary>
    /// パーティに必要なパラメータ
    /// </summary>
    [Serializable]
    public class Parameter
    {
        [SerializeField]
        [Range(1.0f, 100.0f)]
        public int Level;

        [SerializeField]
        public CharacterControllers.Blueprint Blueprint;

        [SerializeField]
        public Battle.Weapon RightWeapon;

        [SerializeField]
        public Battle.Weapon LeftWeapon;

        [SerializeField]
        public Battle.Accessory[] Accessories;

        public static Parameter Create(UserData userData, Player player)
        {
            return new Parameter()
            {
                Level = player.Level,
                Blueprint = player.Blueprint,
                RightWeapon = Database.Weapon.List.Find(w => w.Id == userData.Weapons.GetByInstanceId(player.RightWeaponInstanceId).Id),
                LeftWeapon = Database.Weapon.List.Find(w => w.Id == userData.Weapons.GetByInstanceId(player.LeftWeaponInstanceId).Id),
                Accessories = player.Accessories
            };
        }

        public Commands.Bundle.Implement[] Commands
        {
            get
            {
                return this.RightWeapon.Commands.Concat(this.LeftWeapon.Commands).Select(c => c.Create()).ToArray();
            }
        }
    }
}
