using GL.Battle.Commands.Bundle;
using GL.Battle;
using HK.Framework.Text;
using UnityEngine;
using GL.Database;
using GL.Battle.Commands;
using System;
using HK.GL.Extensions;

namespace GL.Database
{
    /// <summary>
    /// 装備品レコード
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/Record/Equipment")]
    public sealed class EquipmentRecord : ScriptableObject, IMasterDataRecord
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <remarks>
        /// ファイル名そのものをIDとしています
        /// </remarks>
        public string Id => this.name;

        [SerializeField]
        private StringAsset.Finder equipmentName;
        public string EquipmentName => this.equipmentName.Get;

        [SerializeField]
        private int rank;
        public int Rank => this.rank;

        [SerializeField]
        private Constants.EquipmentType equipmentType;
        public Constants.EquipmentType EquipmentType => this.equipmentType;

        [SerializeField]
        private int price;
        /// <summary>
        /// 値段
        /// </summary>
        public int Price => this.price;

        [SerializeField]
        private ConditionalCommandRecord[] commands;
        public ConditionalCommandRecord[] Commands => this.commands;

        [SerializeField]
        private SkillElement[] skillElements;
        public SkillElement[] SkillElements => this.skillElements;

        [SerializeField]
        private NeedMaterial[] needMaterials = new NeedMaterial[0];
        public NeedMaterial[] NeedMaterials => this.needMaterials;

#if UNITY_EDITOR
        public void Set(
            StringAsset.Finder equipmentName,
            int rank,
            Constants.EquipmentType equipmentType,
            int price,
            ConditionalCommandRecord[] commands,
            SkillElement[] skills,
            NeedMaterial[] needMaterials
            )
        {
            this.equipmentName = equipmentName;
            this.rank = rank;
            this.equipmentType = equipmentType;
            this.price = price;
            this.commands = commands;
            this.skillElements = skills;
            this.needMaterials = needMaterials;
        }
#endif
    }
}
