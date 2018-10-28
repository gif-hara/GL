using System;
using System.Linq;
using GL.Battle.CharacterControllers;
using GL.Battle;
using GL.Events.Battle;
using HK.Framework.EventSystems;
using HK.Framework.Text;
using HK.GL.Extensions;
using UnityEngine.Assertions;
using UnityEngine;

namespace GL.Battle.Commands.Bundle
{
    /// <summary>
    /// <see cref="Commands.Blueprints"/>をまとめている実際に実行されるコマンドの実体
    /// </summary>
    public sealed class Implement
    {
        private Parameter parameter;

        private int currentChargeTurn;
        public int CurrentChargeTurn => this.currentChargeTurn;

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
            this.currentChargeTurn = this.parameter.InitialChargeTurn;
            this.elements = parameter.Elements.Select(e => e.Element.Create()).ToArray();
        }

        /// <summary>
        /// コマンド実行
        /// </summary>
        public void Invoke(Character invoker, Character[] targets)
        {
            Assert.IsTrue(this.currentChargeTurn >= this.parameter.ChargeTurn, $"{this.parameter.Name.Get}がチャージターン数を満たしていないのにコマンドが実行されました");
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
                this.currentChargeTurn = -1;
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
        /// チャージターン数を加算する
        /// </summary>
        public void AddChargeTurn(int value)
        {
            this.currentChargeTurn += value;
        }

        /// <summary>
        /// コマンドを実行可能か返す
        /// </summary>
        public bool CanInvoke => this.currentChargeTurn >= this.parameter.ChargeTurn;

        public int ChargeTurn => this.parameter.ChargeTurn;

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
            /// 実行後に必要なチャージターン数
            /// </summary>
            public int ChargeTurn;

            /// <summary>
            /// チャージターン数の初期値
            /// </summary>
            public int InitialChargeTurn;

            /// <summary>
            /// 実行されるコマンドリスト
            /// </summary>
            public ConditionalCommand[] Elements;
        }

        /// <summary>
        /// 条件付きのコマンド
        /// </summary>
        [Serializable]
        public class ConditionalCommand
        {
            [SerializeField]
            private Element.Blueprint element;
            public Element.Blueprint Element => this.element;

            [SerializeField]
            private CommandElementCondition condition;
            public CommandElementCondition Condition => this.condition;
        }
    }
}
