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
using UniRx;
using System.Collections.Generic;
using GL.Battle.UI;

namespace GL.Battle.Commands.Bundle
{
    /// <summary>
    /// <see cref="Commands.Blueprints"/>をまとめている実際に実行されるコマンドの実体
    /// </summary>
    public sealed class Implement
    {
        private Parameter parameter;

        public int CurrentChargeTurn { get; private set; }

        public int ChargeTurn { get; private set; }

        private ImplementList[] elementLists;

        private Element.IImplement[] allElement;

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
            this.CurrentChargeTurn = this.parameter.InitialChargeTurn;
            this.ChargeTurn = this.parameter.ChargeTurn;
            this.elementLists = parameter.ElementLists.Select(e => new ImplementList(e.Create())).ToArray();
            var allElement = new List<Element.IImplement>();
            this.elementLists.ForEach(e => allElement.AddRange(e.Implements));
            this.allElement = allElement.ToArray();
        }

        /// <summary>
        /// コマンド実行
        /// </summary>
        public void Invoke(Character invoker, Character[] targets)
        {
            if(invoker.CharacterType == Constants.CharacterType.Player)
            {
                Assert.IsTrue(this.CurrentChargeTurn >= this.parameter.ChargeTurn, $"{this.parameter.Name.Get}がチャージターン数を満たしていないのにコマンドが実行されました");
            }
            
            if(this.CanRecord)
            {
                BattleManager.Instance.InvokedCommandResult.InvokedCommand = this;
            }

            this.StartAnimation(invoker, targets, 0);
            this.CurrentChargeTurn = -1;
        }

        private void StartAnimation(Character invoker, Character[] targets, int elementListsIndex)
        {
            if(this.elementLists.Length <= elementListsIndex)
            {
                this.Postprocess(invoker)();
            }
            else
            {
                var tuple = new Tuple<Implement, Character, Character[], int>(this, invoker, targets, elementListsIndex);
                invoker.StartAttack(
                () =>
                {
                    var elements = this.elementLists[elementListsIndex].Implements;
                    var finalTargets = this.GetTargetsFromRandom(targets);
                    elements.ForEach(e =>
                    {
                        e.Invoke(invoker, this, finalTargets);
                    });
                })
                .Where(x => x == CharacterUIAnimation.AnimationType.Attack)
                .Take(1)
                .SubscribeWithState(tuple, (_, t) =>
                {
                    var _this = t.Item1;
                    var _invoker = t.Item2;
                    var _targets = t.Item3;
                    var nextElementListsIndex = t.Item4 + 1;
                    _this.StartAnimation(_invoker, _targets, nextElementListsIndex);
                });
            }
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
                case Constants.TargetType.SelectRange:
                case Constants.TargetType.OnCounterTakeDamages:
                    return BattleManager.Instance.Parties
                            .GetFromTargetPartyType(invoker, this.parameter.TargetPartyType)
                            .GetTargets(invoker, this.parameter.TargetType);
                default:
                    Assert.IsTrue(false, $"未対応の値です TargetType = {this.parameter.TargetType}");
                    return null;
            }
        }

        /// <summary>
        /// 必要チャージターン数を減らす
        /// </summary>
        public void SubtractChargeTurn(int amount)
        {
            this.ChargeTurn -= amount;
        }

        /// <summary>
        /// ターゲットの最終選択を行う
        /// </summary>
        /// <remarks>
        /// Randomでない場合はそのまま利用する
        /// </remarks>
        private Character[] GetTargetsFromRandom(Character[] targets)
        {
            if(this.parameter.TargetType != Constants.TargetType.Random)
            {
                return targets;
            }

            var result = new List<Character>();
            var takeDamage = this.allElement.Any(e => e.TakeDamage);
            var protectCharacters = targets.FindAll(c => c.AilmentController.Find(Constants.StatusAilmentType.Protect));
            var canProtect = takeDamage && protectCharacters.Length > 0;
            Character protectCharacter = null;
            if (canProtect)
            {
                protectCharacter = protectCharacters[UnityEngine.Random.Range(0, protectCharacters.Length)];
            }

            var random = targets[UnityEngine.Random.Range(0, targets.Length)];
            if (canProtect && random != protectCharacter)
            {
                result.Add(protectCharacter);
                // TODO: 庇う発動したことを通知する
            }
            else
            {
                result.Add(random);
            }

            return result.ToArray();
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
            this.CurrentChargeTurn += value;
        }

        /// <summary>
        /// コマンドを実行可能か返す
        /// </summary>
        public bool CanInvoke => this.CurrentChargeTurn >= this.ChargeTurn;

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
            public BlueprintList[] ElementLists;

            [Serializable]
            public class BlueprintList
            {
                [SerializeField]
                public List<Element.Blueprint> Elements = new List<Element.Blueprint>();

                public Element.IImplement[] Create()
                {
                    return this.Elements.Select(e => e.Create()).ToArray();
                }
            }

            public void Set(
                StringAsset.Finder name,
                StringAsset.Finder description,
                Constants.TargetPartyType targetPartyType,
                Constants.TargetType targetType,
                Constants.PostprocessCommand postprocess,
                int chargeTurn,
                int initialChargeTurn,
                BlueprintList[] elementLists
            )
            {
                this.Name = name;
                this.Description = description;
                this.TargetPartyType = targetPartyType;
                this.TargetType = targetType;
                this.Postprocess = postprocess;
                this.ChargeTurn = chargeTurn;
                this.InitialChargeTurn = initialChargeTurn;
                this.ElementLists = elementLists;
            }
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
            private EquipmentElementCondition condition;
            public EquipmentElementCondition Condition => this.condition;
        }

        public sealed class ImplementList
        {
            public Element.IImplement[] Implements { get; private set; }

            public ImplementList(Element.IImplement[] implements)
            {
                this.Implements = implements;
            }
        }
    }
}
