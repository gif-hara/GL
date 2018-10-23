using GL.Battle.CharacterControllers;
using GL.Database;
using HK.GL.Extensions;
using UnityEngine;

namespace GL.Battle
{
    /// <summary>
    /// キャラクターが装備できるアクセサリー
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Accessories/Accessory")]
    public sealed class Accessory : ScriptableObject, IMasterDataRecord
    {
        public string Id => this.name;
        
        [SerializeField]
        private AccessoryElement[] elements = new AccessoryElement[0];

        public void OnStartBattle(CharacterControllers.Character equippedCharacter)
        {
            this.elements.ForEach(e => e.OnStartBattle(equippedCharacter));
        }
    }
}
