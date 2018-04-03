using GL.Scripts.Battle.Commands.Impletents;
using GL.Scripts.Battle.Systems;
using HK.Framework.Text;
using UnityEngine;

namespace GL.Scripts.Battle.Commands.Blueprints
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
