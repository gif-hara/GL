using System.Collections.Generic;
using GL.Battle.AIControllers;
using GL.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.CharacterControllers
{
    /// <summary>
    /// AIを制御するクラス
    /// </summary>
    public sealed class AIController
    {
        private Character owner;

        private AI ai;

        /// <summary>
        /// このキャラクターに対して何かしら影響を与えたキャラクター
        /// </summary>
        /// <remarks>
        /// ダメージを与えたりステータスを変化させたキャラクターが入ります
        /// </remarks>
        private Character affectedCharacter;
        public Character AffectedCharacter => this.affectedCharacter;

        private int currentCommandSelectorListIndex;

        private int currentOnEndTurnEventSelectorListIndex;

        private List<CommandSelector> CurrentCommandSelectors => this.ai.CommandSelectorLists[this.currentCommandSelectorListIndex].CommandSelectors;

        private List<EventSelector> CurrentOnEndTurnEventSelectors => this.ai.OnEndTurnEventSelectorLists[this.currentOnEndTurnEventSelectorListIndex].EventSelectors;

        public int InvokedCount { get; private set; }

        public AIController(Character owner, AI ai)
        {
            this.owner = owner;
            this.ai = ai;
            this.currentCommandSelectorListIndex = 0;
            this.currentOnEndTurnEventSelectorListIndex = 0;

            Broker.Global.Receive<DamageNotify>()
                .Where(x => x.Receiver == this.owner)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.affectedCharacter = x.Invoker;
                })
                .AddTo(this.owner);

            Broker.Global.Receive<EndTurn>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    if(!_this.owner.StatusController.IsDead)
                    {
                        _this.InvokeEndTurnEvent();
                    }
                    if(x.Character == _this.owner)
                    {
                        _this.InvokedCount++;
                    }
                })
                .AddTo(this.owner);

            Broker.Global.Receive<NextTurn>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.affectedCharacter = null;
                })
                .AddTo(this.owner);
        }

        public void InvokeCommand()
        {
            foreach (var b in this.CurrentCommandSelectors)
            {
                if (b.Suitable(this.owner))
                {
                    b.Invoke(this.owner);
                    return;
                }
            }

            Assert.IsTrue(false, $"{this.owner.StatusController.Name}が実行出来るコマンドがありませんでした");
        }

        public void InvokeEndTurnEvent()
        {
            foreach(var s in this.CurrentOnEndTurnEventSelectors)
            {
                if(!s.Suitable(this.owner))
                {
                    continue;
                }
                foreach(var e in s.Events)
                {
                    e.Invoke(this.owner);
                }
            }
        }

        public void ChangeCommandSelector(int index)
        {
            this.currentCommandSelectorListIndex = index;
        }

        public void ChangeOnEndTurnEventSelector(int index)
        {
            this.currentOnEndTurnEventSelectorListIndex = index;
        }
    }
}
