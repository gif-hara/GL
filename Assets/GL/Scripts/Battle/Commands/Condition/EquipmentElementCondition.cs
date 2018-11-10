using GL.Battle.CharacterControllers;
using GL.Database;
using GL.User;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.Commands
{
    /// <summary>
    /// 装備品のコマンドやスキルが実行出来る条件を持つクラス
    /// </summary>
    public abstract class EquipmentElementCondition : ScriptableObject
    {
        [SerializeField]
        protected StringAsset.Finder description;
        public virtual string Description => this.description.Get;

        /// <summary>
        /// 条件を満たしているか返す
        /// </summary>
        public abstract bool Suitable(EquipmentRecord rightWeapon, EquipmentRecord leftWeapon, EquipmentRecord[] accessories);
    }
}
