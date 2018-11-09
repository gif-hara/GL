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
        public CharacterRecord CharacterRecord;

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
                CharacterRecord = player.CharacterRecord,
                RightWeapon = player.RightHand.EquipmentRecord,
                LeftWeapon = player.LeftHand.EquipmentRecord,
                Accessories = player.Accessories,
            };
        }

        public Commands.Bundle.Implement[] Commands => Calculator.GetCommandRecords(this.RightWeapon, this.LeftWeapon, this.Accessories).Select(r => r.CommandRecord.Create()).ToArray();

        public SkillElement[] SkillElements
        {
            get
            {
                var result = new List<SkillElement>();
                this.AddSkillElements(result, this.RightWeapon);
                this.AddSkillElements(result, this.LeftWeapon);
                foreach(var a in this.Accessories)
                {
                    this.AddSkillElements(result, a);
                }

                return result.ToArray();
            }
        }

        private void AddSkillElements(List<SkillElement> skillElements, EquipmentRecord equipmentRecord)
        {
            if(equipmentRecord == null)
            {
                return;
            }

            foreach (var s in equipmentRecord.SkillElements)
            {
                if (s.Condition.Suitable(this.RightWeapon, this.LeftWeapon, this.Accessories))
                {
                    skillElements.Add(s.SkillElement);
                }
            }
        }

#if UNITY_EDITOR
        public void Set(int level, CharacterRecord characterRecord, EquipmentRecord rightWeapon, EquipmentRecord leftWeapon, EquipmentRecord[] accessories)
        {
            this.Level = level;
            this.CharacterRecord = characterRecord;
            this.RightWeapon = rightWeapon;
            this.LeftWeapon = leftWeapon;
            this.Accessories = accessories;
        }
#endif
    }
}
