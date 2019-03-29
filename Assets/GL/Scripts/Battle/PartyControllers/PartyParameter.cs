using System;
using System.Linq;
using GL.Database;
using GL.User;
using UnityEngine;

namespace GL.Battle
{
    /// <summary>
    /// パーティに必要なパラメータ
    /// </summary>
    [Serializable]
    public class PartyParameter
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

        public static PartyParameter Create(Player player)
        {
            return new PartyParameter()
            {
                Level = player.Level,
                CharacterRecord = player.CharacterRecord,
                RightWeapon = player.RightHand.EquipmentRecord,
                LeftWeapon = player.LeftHand.EquipmentRecord,
                Accessories = player.Accessories,
            };
        }

        public Commands.Bundle.Implement[] Commands => Calculator.GetCommandRecords(this.RightWeapon, this.LeftWeapon, this.Accessories).Select(r => r.CommandRecord.Create()).ToArray();

        public SkillElement[] SkillElements => Calculator.GetSkillElements(this.RightWeapon, this.LeftWeapon, this.Accessories).Select(s => s.SkillElement).ToArray();

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
