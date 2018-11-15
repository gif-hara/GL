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

        private CommandSelector[] currentCommandSelectors;

        private EventSelector[] currentOnEndTurnEventSelectors;

        public int InvokedCount { get; private set; }

        public AIController(Character owner, AI ai)
        {
            this.owner = owner;
            this.ai = ai;
            this.currentCommandSelectors = this.ai.DefaultCommandSelectors;
            this.currentOnEndTurnEventSelectors = this.ai.DefaultOnEndTurnEventSelectors;

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
            foreach (var b in this.currentCommandSelectors)
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
            foreach(var s in this.currentOnEndTurnEventSelectors)
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

        public void ChangeCommandSelector(CommandSelector[] commandSelectors)
        {
            this.currentCommandSelectors = commandSelectors;
        }

        public void ChangeOnEndTurnEventSelector(EventSelector[] eventSelectors)
        {
            this.currentOnEndTurnEventSelectors = eventSelectors;
        }
    }
}
