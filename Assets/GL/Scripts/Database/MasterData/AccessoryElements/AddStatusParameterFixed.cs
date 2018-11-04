using GL.Battle.CharacterControllers;
using GL.Battle;
using UnityEngine;
using HK.Framework.Text;

namespace GL.Database
{
    /// <summary>
    /// ステータスを固定値で上昇させるアクセサリー
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Accessories/AddStatusParameterFixed")]
    public sealed class AddStatusParameterFixed : SkillElement
    {
        [SerializeField]
        private Constants.StatusParameterType statusParameterType;

        /// <summary>
        /// 上昇させるステータスタイプ
        /// </summary>
        public Constants.StatusParameterType StatusParameterType { get { return statusParameterType; } }

        [SerializeField]
        private int amount;

        /// <summary>
        /// 上昇量
        /// </summary>
        public int Amount { get { return amount; } }
        
        public override void OnStartBattle(Character equippedCharacter)
        {
            equippedCharacter.StatusController.AddParameterToAccessory(this.StatusParameterType, this.amount);
        }

#if UNITY_EDITOR
        public AddStatusParameterFixed Setup(
            StringAsset.Finder elementName,
            StringAsset.Finder description,
            Constants.StatusParameterType statusParameterType,
            int amount
            )
        {
            this.elementName = elementName;
            this.description = description;
            this.statusParameterType = statusParameterType;
            this.amount = amount;

            return this;
        }
#endif
    }
}
