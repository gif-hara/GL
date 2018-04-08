using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using UnityEngine;

namespace GL.Scripts.Battle.Accessories
{
    /// <summary>
    /// 状態異常の耐性を上昇させるアクセサリー
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Accessories/AddStatusResistance")]
    public sealed class AddStatusResistance : Accessory
    {
        [SerializeField]
        private Constants.StatusAilmentType statusAilmentType;

        [SerializeField]
        private float rate;

        public override void OnStartBattle(Character equippedCharacter)
        {
            equippedCharacter.StatusController.AddResistanceToAccessory(this.statusAilmentType, this.rate);
        }
    }
}
