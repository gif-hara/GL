using System;
using GL.Scripts.Battle.Systems;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Scripts.Battle.Commands.Bundle
{
    /// <summary>
    /// <see cref="Commands.Blueprints"/>をまとめている実際に実行されるコマンドの実体
    /// </summary>
    public sealed class Implement
    {
        private Parameter parameter;

        public string Name
        {
            get
            {
                return this.parameter.Name.Get;
            }
        }

        public Implement(Parameter parameter)
        {
            this.parameter = parameter;
        }

        [Serializable]
        public class Parameter
        {
            /// <summary>
            /// コマンド名
            /// </summary>
            public StringAsset.Finder Name;

            /// <summary>
            /// 対象となるパーティタイプ
            /// </summary>
            public Constants.TargetPartyType TargetPartyType;

            /// <summary>
            /// ターゲットタイプ
            /// </summary>
            /// <remarks>
            /// 誰を狙うかを決めるのに必要
            /// </remarks>
            public Constants.TargetType TargetType;

            /// <summary>
            /// 実行されるコマンドリスト
            /// </summary>
            public Blueprints.Blueprint[] elements;
        }
    }
}
