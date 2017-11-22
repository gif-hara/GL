using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace HK.GL.Battle
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
