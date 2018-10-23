using System;
using System.Collections.Generic;
using System.Linq;
using GL.Battle;
using GL.Database;
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
        public WeaponRecord RightWeapon;

        [SerializeField]
        public WeaponRecord LeftWeapon;

        [SerializeField]
        public Battle.Accessory[] Accessories;

        public static Parameter Create(UserData userData, Player player)
        {
            return new Parameter()
            {
                Level = player.Level,
                Blueprint = player.Blueprint,
                RightWeapon = player.RightHand.BattleWeapon,
                LeftWeapon = player.LeftHand.BattleWeapon,
                Accessories = player.Accessories
            };
        }

        public Commands.Bundle.Implement[] Commands
        {
            get
            {
                var result = new List<Commands.Bundle.Implement>();
                if(this.RightWeapon != null)
                {
                    result.AddRange(this.RightWeapon.Commands.Select(c => c.Create()));
                }
                if(this.LeftWeapon != null)
                {
                    result.AddRange(this.LeftWeapon.Commands.Select(c => c.Create()));
                }

                return result.ToArray();
            }
        }
    }
}
