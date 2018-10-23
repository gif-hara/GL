using GL.Battle.CharacterControllers;
using GL.Database;
using HK.GL.Extensions;
using UnityEngine;

namespace GL.Database
{
    /// <summary>
    /// キャラクターが装備できるアクセサリー
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/Record/Accessory")]
    public sealed class AccessoryRecord : ScriptableObject, IMasterDataRecord
    {
        public string Id => this.name;
        
        [SerializeField]
        private AccessoryElement[] elements = new AccessoryElement[0];

        public void OnStartBattle(Battle.CharacterControllers.Character equippedCharacter)
        {
            this.elements.ForEach(e => e.OnStartBattle(equippedCharacter));
        }
    }
}
