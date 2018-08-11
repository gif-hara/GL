using GL.Battle.CharacterControllers;
using UnityEngine;

namespace GL.Battle.Accessories
{
    /// <summary>
    /// キャラクターが装備できるアクセサリーの抽象クラス
    /// </summary>
    public abstract class Accessory : ScriptableObject
    {
        public abstract void OnStartBattle(Character equippedCharacter);
    }
}
