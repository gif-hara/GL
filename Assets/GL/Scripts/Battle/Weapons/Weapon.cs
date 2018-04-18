using GL.Scripts.Battle.Commands.Blueprints;
using GL.Scripts.Battle.Systems;
using HK.Framework.Text;
using UnityEngine;

namespace GL.Scripts.Battle.Weapons
{
    /// <summary>
    /// 武器クラス
    /// </summary>
    /// <remarks>
    /// コマンドリストを担う
    /// </remarks>
    [CreateAssetMenu(menuName = "GL/Weapon")]
    public sealed class Weapon : ScriptableObject
    {
        [SerializeField]
        private StringAsset.Finder weaponName;
        public string WeaponName { get { return weaponName.Get; } }
        
        [SerializeField]
        private Constants.WeaponType weaponType;
        public Constants.WeaponType WeaponType { get { return weaponType; } }

        
        [SerializeField]
        private Blueprint[] commands;
        public Blueprint[] Commands { get { return commands; } }
    }
}
