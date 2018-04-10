using GL.Scripts.Battle.Accessories;
using Blueprint = GL.Scripts.Battle.PartyControllers.Blueprints.Blueprint;

namespace GL.Scripts.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターが装備するアクセサリーを制御するクラス
    /// </summary>
    public sealed class CharacterAccessoryController
    {
        public Accessory[] Accessories { get; private set; }
        
        public CharacterAccessoryController(Blueprints.Blueprint blueprint)
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
