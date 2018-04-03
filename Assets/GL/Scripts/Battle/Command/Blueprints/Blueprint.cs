using GL.Scripts.Battle.Command.Impletents;
using HK.Framework.Text;
using HK.GL.Battle;
using UnityEngine;

namespace GL.Scripts.Battle.Command.Blueprints
{
    /// <summary>
    /// コマンドを構成する設計図の抽象クラス
    /// </summary>
    public abstract class Blueprint : ScriptableObject
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
        /// 設計図からコマンドを作成する
        /// </summary>
        public abstract IImplement Create();
    }
}
