using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Database
{
    /// <summary>
    /// 素手のコマンドや狂暴攻撃などのコマンドを置いておくクラス
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/ConstantCommand")]
    public sealed class ConstantCommand :ScriptableObject
    {
        [SerializeField]
        private CommandRecord unequipment;

        /// <summary>
        /// 素手のコマンド
        /// </summary>
        public CommandRecord Unequipment => this.unequipment;
    }
}
