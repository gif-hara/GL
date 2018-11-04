using GL.Battle.CharacterControllers;
using GL.Database;
using GL.User;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.Commands
{
    /// <summary>
    /// コマンドが実行出来る条件を持つクラス
    /// </summary>
    public abstract class CommandElementCondition : ScriptableObject
    {
        [SerializeField]
        private StringAsset.Finder description;
        public StringAsset.Finder Description => this.description;

        /// <summary>
        /// 条件を満たしているか返す
        /// </summary>
        public abstract bool Suitable(EquipmentRecord rightWeapon, EquipmentRecord leftWeapon, EquipmentRecord[] accessories);
    }
}
