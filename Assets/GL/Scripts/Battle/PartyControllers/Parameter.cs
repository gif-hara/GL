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
        public CharacterRecord Blueprint;

        [SerializeField]
        public WeaponRecord RightWeapon;

        [SerializeField]
        public WeaponRecord LeftWeapon;

        [SerializeField]
        public AccessoryRecord[] Accessories;

        public static Parameter Create(Player player)
        {
            return new Parameter()
            {
                Level = player.Level,
                Blueprint = player.CharacterRecord,
                RightWeapon = player.RightHand.BattleWeapon,
                LeftWeapon = player.LeftHand.BattleWeapon,
                Accessories = player.Accessories
            };
        }

        public Commands.Bundle.Implement[] Commands
        {
            get
            {
                var constantCommand = MasterData.ConstantCommand;
                var result = new List<Commands.Bundle.Implement>();

                // 何も装備していないときは素手コマンドを追加
                if(this.RightWeapon == null && this.LeftWeapon == null)
                {
                    result.Add(constantCommand.Unequipment.Create());
                }

                if(this.RightWeapon != null)
                {
                    result.AddRange(this.RightWeapon.Commands.Select(c => c.CommandRecord.Create()));
                }
                if(this.LeftWeapon != null)
                {
                    result.AddRange(this.LeftWeapon.Commands.Select(c => c.CommandRecord.Create()));
                }

                return result.ToArray();
            }
        }
    }
}
