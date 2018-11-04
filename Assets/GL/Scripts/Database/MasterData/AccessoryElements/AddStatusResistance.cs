using GL.Battle.CharacterControllers;
using GL.Battle;
using UnityEngine;

namespace GL.Database
{
    /// <summary>
    /// 状態異常の耐性を上昇させるアクセサリー
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Accessories/AddStatusResistance")]
    public sealed class AddStatusResistance : SkillElement
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
