using GL.Battle.CharacterControllers;
using GL.Battle;
using UnityEngine;
using HK.Framework.Text;

namespace GL.Database
{
    /// <summary>
    /// ステータスを割合で上昇させるアクセサリー
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Accessories/AddStatusParameterRate")]
    public sealed class AddStatusParameterRate : SkillElement
    {
        [SerializeField]
        private Constants.StatusParameterType statusParameterType;

        /// <summary>
        /// 上昇させるステータスタイプ
        /// </summary>
        public Constants.StatusParameterType StatusParameterType => this.statusParameterType;

        [SerializeField]
        private float rate;

        /// <summary>
        /// 上昇量
        /// </summary>
        public float Rate => this.rate;

        public override void OnStartBattle(Character equippedCharacter)
        {
            var amount = Mathf.FloorToInt(equippedCharacter.StatusController.Base.Parameter.Get(this.StatusParameterType) * this.Rate);
            equippedCharacter.StatusController.AddParameterToAccessory(this.StatusParameterType, amount);
        }

#if UNITY_EDITOR
        public AddStatusParameterRate Setup(
            StringAsset.Finder elementName,
            StringAsset.Finder description,
            Constants.StatusParameterType statusParameterType,
            float rate
            )
        {
            this.elementName = elementName;
            this.description = description;
            this.statusParameterType = statusParameterType;
            this.rate = rate;

            return this;
        }
#endif
    }
}
