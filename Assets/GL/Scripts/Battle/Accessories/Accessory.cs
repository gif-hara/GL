using GL.Battle.CharacterControllers;
using HK.GL.Extensions;
using UnityEngine;

namespace GL.Battle.Accessories
{
    /// <summary>
    /// キャラクターが装備できるアクセサリー
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Accessories/Accessory")]
    public sealed class Accessory : ScriptableObject
    {
        [SerializeField]
        private Element[] elements = new Element[0];

        public void OnStartBattle(Character equippedCharacter)
        {
            this.elements.ForEach(e => e.OnStartBattle(equippedCharacter));
        }
    }
}
