using HK.Framework.Text;
using HK.GL.Battle;
using UnityEngine;

namespace GL.Scripts.Battle.Command.Settings
{
    /// <summary>
    /// コマンドの設定データ.
    /// </summary>
    public abstract class CommandSettings : ScriptableObject
    {
        /// <summary>
        /// コマンド名
        /// </summary>
        [SerializeField]
        protected StringAsset.Finder commandName;

        /// <summary>
        /// ターゲットタイプ
        /// </summary>
        [SerializeField]
        protected Constants.TargetType targetType;

        /// <summary>
        /// 設定データからコマンドを作成する
        /// </summary>
        public abstract ICommand Create();
    }
}
