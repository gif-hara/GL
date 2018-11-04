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
        public EquipmentRecord RightWeapon;

        [SerializeField]
        public EquipmentRecord LeftWeapon;

        [SerializeField]
        public EquipmentRecord[] Accessories;

        public static Parameter Create(Player player)
        {
            return new Parameter()
            {
                Level = player.Level,
                Blueprint = player.CharacterRecord,
                RightWeapon = player.RightHand.EquipmentRecord,
                LeftWeapon = player.LeftHand.EquipmentRecord,
                Accessories = player.Accessories,
            };
        }

        public Commands.Bundle.Implement[] Commands
        {
            get
            {
                var constantCommand = MasterData.ConstantCommand;
                var result = new List<CommandRecord>();

                // 何も装備していないときは素手コマンドを追加
                if (this.RightWeapon == null && this.LeftWeapon == null)
                {
                    result.Add(constantCommand.Unequipment);
                }

                if (this.RightWeapon != null)
                {
                    foreach (var c in this.RightWeapon.Commands)
                    {
                        if (!result.Contains(c.CommandRecord) && c.Condition.Suitable(this.RightWeapon, this.LeftWeapon, this.Accessories))
                        {
                            result.Add(c.CommandRecord);
                        }
                    }
                }
                if (this.LeftWeapon != null)
                {
                    foreach (var c in this.LeftWeapon.Commands)
                    {
                        if (!result.Contains(c.CommandRecord) && c.Condition.Suitable(this.RightWeapon, this.LeftWeapon, this.Accessories))
                        {
                            result.Add(c.CommandRecord);
                        }
                    }
                }
                foreach(var accessory in this.Accessories)
                {
                    if(accessory == null)
                    {
                        continue;
                    }

                    foreach (var c in accessory.Commands)
                    {
                        if (!result.Contains(c.CommandRecord) && c.Condition.Suitable(this.RightWeapon, this.LeftWeapon, this.Accessories))
                        {
                            result.Add(c.CommandRecord);
                        }
                    }
                }

                return result.Select(r => r.Create()).ToArray();
            }
        }

        public SkillElement[] SkillElements
        {
            get
            {
                var result = new List<SkillElement>();
                if(this.RightWeapon != null)
                {
                    result.AddRange(this.RightWeapon.SkillElements);
                }
                if(this.LeftWeapon != null)
                {
                    result.AddRange(this.LeftWeapon.SkillElements);
                }
                this.Accessories.ForEach(a =>
                {
                    if (a == null)
                    {
                        return;
                    }

                    result.AddRange(a.SkillElements);
                });

                return result.ToArray();
            }
        }
    }
}
