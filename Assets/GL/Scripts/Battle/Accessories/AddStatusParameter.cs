using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using UnityEngine;

namespace GL.Scripts.Battle.Accessories
{
    /// <summary>
    /// ステータスを上昇させるアクセサリー
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Accessories/AddStatusParameter")]
    public sealed class AddStatusParameter : Accessory
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
    }
}
