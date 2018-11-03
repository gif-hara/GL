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
    /// 武器レコード
    /// </summary>
    /// <remarks>
    /// コマンドリストを担う
    /// </remarks>
    [CreateAssetMenu(menuName = "GL/MasterData/Record/Weapon")]
    public sealed class WeaponRecord : ScriptableObject, IMasterDataRecord
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <remarks>
        /// ファイル名そのものをIDとしています
        /// </remarks>
        public string Id => this.name;

        [SerializeField]
        private StringAsset.Finder weaponName;
        public string WeaponName => this.weaponName.Get;

        [SerializeField]
        private int rank;
        public int Rank => this.rank;

        [SerializeField]
        private Constants.WeaponType weaponType;
        public Constants.WeaponType WeaponType => this.weaponType;

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
        private NeedMaterial[] needMaterials = new NeedMaterial[0];
        public NeedMaterial[] NeedMaterials => this.needMaterials;

#if UNITY_EDITOR
        public void Set(StringAsset.Finder weaponName, int rank, Constants.WeaponType weaponType, int price, ConditionalCommandRecord[] commands, NeedMaterial[] needMaterials)
        {
            this.weaponName = weaponName;
            this.rank = rank;
            this.weaponType = weaponType;
            this.price = price;
            this.commands = commands;
            this.needMaterials = needMaterials;
        }
#endif
    }
}
