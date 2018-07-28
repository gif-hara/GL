using GL.Scripts.Battle.Accessories;
using UnityEngine;
using Blueprint = GL.Scripts.Battle.PartyControllers.Blueprints.Blueprint;

namespace GL.Scripts.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターが装備するアクセサリーを制御するクラス
    /// </summary>
    public sealed class CharacterAccessoryController
    {
        public Accessory[] Accessories { get; private set; }
        
        public CharacterAccessoryController()
        {
            Debug.LogWarning("アクセサリー設定を実装");
            this.Accessories = new Accessory[0];
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
