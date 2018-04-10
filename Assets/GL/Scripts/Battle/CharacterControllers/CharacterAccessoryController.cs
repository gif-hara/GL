using GL.Scripts.Battle.Accessories;
using GL.Scripts.Battle.CharacterControllers.Blueprints;

namespace GL.Scripts.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターが装備するアクセサリーを制御するクラス
    /// </summary>
    public sealed class CharacterAccessoryController
    {
        public Accessory[] Accessories { get; private set; }
        
        public CharacterAccessoryController(Blueprint blueprint)
        {
            this.Accessories = blueprint.Accessories;
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
