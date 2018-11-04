using GL.Battle.CharacterControllers;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Database
{
    /// <summary>
    /// スキルを構成するクラス
    /// </summary>
    public abstract class SkillElement : ScriptableObject
    {
        [SerializeField]
        private StringAsset.Finder elementName;
        public string ElementName => this.elementName.Get;

        [SerializeField]
        private StringAsset.Finder description;
        public string Description => this.description.Get;

        /// <summary>
        /// バトル開始時に行う処理
        /// </summary>
        public abstract void OnStartBattle(Character equippedCharacter);
    }
}
