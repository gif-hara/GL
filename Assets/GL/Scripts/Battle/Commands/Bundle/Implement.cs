using System;
using System.Linq;
using GL.Battle.CharacterControllers;
using GL.Battle;
using GL.Events.Battle;
using HK.Framework.EventSystems;
using HK.Framework.Text;
using HK.GL.Extensions;
using UnityEngine.Assertions;

namespace GL.Battle.Commands.Bundle
{
    /// <summary>
    /// <see cref="Commands.Blueprints"/>をまとめている実際に実行されるコマンドの実体
    /// </summary>
    public sealed class Implement
    {
        private Parameter parameter;

        private Element.IImplement[] elements;

        public string Name
        {
            get { return this.parameter.Name.Get; }
        }

        public Constants.TargetType TargetType
        {
            get { return this.parameter.TargetType; }
        }

        public Implement(Parameter parameter)
        {
            this.parameter = parameter;
            this.elements = parameter.Elements.Select(e => e.Create()).ToArray();
        }

        /// <summary>
        /// コマンド実行
        /// </summary>
        public void Invoke(Character invoker, Character[] targets)
        {
            if(this.CanRecord)
            {
                BattleManager.Instance.InvokedCommandResult.InvokedCommand = this;
            }

            invoker.StartAttack(
            () =>
            {
                this.elements.ForEach(e =>
                {
                    e.Invoke(invoker, this, targets);
                });
            },
            () =>
            {
                this.Postprocess(invoker)();
            });
        }

        /// <summary>
        /// コマンドを実行する対象を返す
        /// </summary>
        public Character[] GetTargets(Character invoker)
        {
            switch (this.parameter.TargetType)
            {
                case Constants.TargetType.Select:
                case Constants.TargetType.All:
                case Constants.TargetType.Random:
                case Constants.TargetType.Myself:
                case Constants.TargetType.OnChaseTakeDamages:
                    var takeDamage = this.elements.Any(e => e.TakeDamage);
                    return BattleManager.Instance.Parties
                            .GetFromTargetPartyType(invoker, this.parameter.TargetPartyType)
                            .GetTargets(invoker, this.parameter.TargetType, takeDamage);
                default:
                    Assert.IsTrue(false, $"未対応の値です TargetType = {this.parameter.TargetType}");
                    return null;
            }
        }

        /// <summary>
        /// <see cref="InvokedCommandResult"/>に登録可能か返す
        /// </summary>
        public bool CanRecord
        {
            get { return this.parameter.Postprocess == Constants.PostprocessCommand.EndTurn; }
        }

        /// <summary>
        /// コマンド実行後の後始末
        /// </summary>
        private Action Postprocess(Character invoker)
        {
            switch (this.parameter.Postprocess)
            {
                case Constants.PostprocessCommand.EndTurn:
                    return () => BattleManager.Instance.EndTurn(invoker);
                case Constants.PostprocessCommand.CompleteEndTurnEvent:
                    return () => Broker.Global.Publish(CompleteEndTurnEvent.Get());
                default:
                    Assert.IsTrue(false, string.Format("未対応の値です {0}", this.parameter.Postprocess));
                    return null;
            }
        }

        [Serializable]
        public class Parameter
        {
            /// <summary>
            /// コマンド名
            /// </summary>
            public StringAsset.Finder Name;

            /// <summary>
            /// 説明
            /// </summary>
            public StringAsset.Finder Description;

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
            /// コマンド実行後の処理タイプ
            /// </summary>
            public Constants.PostprocessCommand Postprocess;

            /// <summary>
            /// 実行されるコマンドリスト
            /// </summary>
            public Element.Blueprint[] Elements;
        }
    }
}
