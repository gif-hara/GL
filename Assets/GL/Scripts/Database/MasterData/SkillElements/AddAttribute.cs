using GL.Battle.CharacterControllers;
using GL.Battle;
using UnityEngine;
using HK.Framework.Text;

namespace GL.Database
{
    /// <summary>
    /// 属性値を上昇させるアクセサリー
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Accessories/AddAttribute")]
    public sealed class AddAttribute : SkillElement
    {
        [SerializeField]
        private Constants.AttributeType attributeType;

        [SerializeField]
        private float rate;

        public override void OnStartBattle(Character equippedCharacter)
        {
            equippedCharacter.StatusController.AddAttributeToAccessory(this.attributeType, this.rate);
        }

#if UNITY_EDITOR
        public AddAttribute Setup(
            StringAsset.Finder elementName,
            StringAsset.Finder description,
            Constants.AttributeType attributeType,
            float rate
            )
        {
            this.elementName = elementName;
            this.description = description;
            this.attributeType = attributeType;
            this.rate = rate;

            return this;
        }
#endif
    }
}
