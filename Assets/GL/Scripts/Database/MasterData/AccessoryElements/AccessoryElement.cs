using GL.Battle.CharacterControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Database
{
    /// <summary>
    /// アクセサリーの効果を構成するクラス
    /// </summary>
    public abstract class AccessoryElement : ScriptableObject
    {
        /// <summary>
        /// バトル開始時に行う処理
        /// </summary>
        public abstract void OnStartBattle(Character equippedCharacter);
    }
}
