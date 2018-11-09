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

        [SerializeField]
        private CommandRecord confuse;

        [SerializeField]
        private CommandRecord berserk;

        [SerializeField]
        private CommandRecord chase;

        /// <summary>
        /// 素手のコマンド
        /// </summary>
        public CommandRecord Unequipment => this.unequipment;

        /// <summary>
        /// 混乱コマンド
        /// </summary>
        public CommandRecord Confuse => this.confuse;

        /// <summary>
        /// 狂暴コマンド
        /// </summary>
        public CommandRecord Berserk => this.berserk;

        /// <summary>
        /// 追討コマンド
        /// </summary>
        public CommandRecord Chase => this.chase;
    }
}
