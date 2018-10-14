using GL.Battle.CharacterControllers;
using HK.GL.Extensions;
using UnityEngine;

namespace GL.Battle
{
    /// <summary>
    /// キャラクターが装備できるアクセサリー
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Accessories/Accessory")]
    public sealed class Accessory : ScriptableObject
    {
        [SerializeField]
        private AccessoryElement[] elements = new AccessoryElement[0];

        public void OnStartBattle(Character equippedCharacter)
        {
            this.elements.ForEach(e => e.OnStartBattle(equippedCharacter));
        }
    }
}
