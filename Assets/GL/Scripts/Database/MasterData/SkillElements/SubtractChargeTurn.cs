using GL.Battle.CharacterControllers;
using GL.Battle;
using UnityEngine;
using HK.Framework.Text;
using HK.GL.Extensions;

namespace GL.Database
{
    /// <summary>
    /// チャージターン数を減算させるアクセサリー
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Accessories/SubtractChargeTurn")]
    public sealed class SubtractChargeTurn : SkillElement
    {
        [SerializeField]
        private int amount;

        public override void OnStartBattle(Character equippedCharacter)
        {
            equippedCharacter.StatusController.Commands.ForEach(c => c.SubtractChargeTurn(this.amount));
        }

#if UNITY_EDITOR
        public SubtractChargeTurn Setup(
            StringAsset.Finder elementName,
            StringAsset.Finder description,
            int amount
            )
        {
            this.elementName = elementName;
            this.description = description;
            this.amount = amount;

            return this;
        }
#endif
    }
}
