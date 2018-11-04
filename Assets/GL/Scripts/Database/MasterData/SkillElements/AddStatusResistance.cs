using GL.Battle.CharacterControllers;
using GL.Battle;
using UnityEngine;
using HK.Framework.Text;

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

#if UNITY_EDITOR
        public AddStatusResistance Setup(
            StringAsset.Finder elementName,
            StringAsset.Finder description,
            Constants.StatusAilmentType statusAilmentType,
            float rate
            )
        {
            this.elementName = elementName;
            this.description = description;
            this.statusAilmentType = statusAilmentType;
            this.rate = rate;

            return this;
        }
#endif
    }
}
