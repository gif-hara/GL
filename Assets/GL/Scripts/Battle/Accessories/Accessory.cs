using GL.Scripts.Battle.CharacterControllers;
using UnityEngine;

namespace GL.Scripts.Battle.Accessories
{
    /// <summary>
    /// キャラクターが装備できるアクセサリーの抽象クラス
    /// </summary>
    public abstract class Accessory : ScriptableObject
    {
        public abstract void OnStartBattle(Character equippedCharacter);
    }
}
