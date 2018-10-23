using GL.Battle;
using GL.Database;
using UnityEngine;

namespace GL.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターが装備するアクセサリーを制御するクラス
    /// </summary>
    public sealed class CharacterAccessoryController
    {
        public AccessoryRecord[] Accessories { get; private set; }
        
        public CharacterAccessoryController(AccessoryRecord[] accessories)
        {
            this.Accessories = accessories;
        }

        public void OnStartBattle(Character equippedCharacter)
        {
            foreach (var accessory in this.Accessories)
            {
                accessory.OnStartBattle(equippedCharacter);
            }
        }
    }
}
