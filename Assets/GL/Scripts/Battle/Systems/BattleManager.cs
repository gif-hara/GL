using System;
using System.Collections.Generic;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Commands.Implements;
using GL.Scripts.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using Blueprint = GL.Scripts.Battle.Commands.Blueprints.Blueprint;

namespace GL.Scripts.Battle.Systems
{
    /// <summary>
    /// バトルを管理するヤーツ.
    /// </summary>
    [RequireComponent(typeof(BehavioralOrderController))]
    public sealed class BattleManager : MonoBehaviour
    {
        /// <summary>
        /// 混乱した時のコマンド
        /// </summary>
        [SerializeField]
        private Blueprint confuseBlueprint;

        /// <summary>
        /// 狂暴した時のコマンド
        /// </summary>
        [SerializeField]
        private Blueprint berserkBlueprint;

        private IImplement confuseCommand;
        public IImplement ConfuseCommand { get { return confuseCommand; } }

        private IImplement berserkCommand;
        public IImplement BerserkCommand { get { return berserkCommand; } }
        
        public static BattleManager Instance { private set; get; }

        public Parties Parties { private set; get; }

        public BehavioralOrderController BehavioralOrder { private set; get; }
        
        public readonly Queue<Action> EndTurnEvents = new Queue<Action>();

        public int TurnNumber { get; private set; }

        public readonly InvokedCommandResult InvokedCommandResult = new InvokedCommandResult();

        void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;

            this.confuseCommand = this.confuseBlueprint.Create();
            this.berserkCommand = this.berserkBlueprint.Create();

            this.BehavioralOrder = this.GetComponent<BehavioralOrderController>();
            
            // FIXME: リザルト実装したら削除する
            Broker.Global.Receive<EndBattle>()
                .Take(1)
                .Subscribe(x => Debug.Log(x.Result))
                .AddTo(this);

            Broker.Global.Receive<CompleteEndTurnEvent>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    if (_this.EndTurnEvents.Count > 0)
                    {
                        _this.InvokeEndTurnEvent();
                    }
                    else
                    {
                        _this.Judgement();
                    }
                })
                .AddTo(this);

            Broker.Global.Receive<EndTurn>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    Debug.Log(_this.InvokedCommandResult.ToString());
                })
                .AddTo(this);

            Broker.Global.Receive<NextTurn>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.TurnNumber++;
                    _this.InvokedCommandResult.Reset();
                })
                .AddTo(this);
        }

        void OnDestroy()
        {
            Instance = null;
        }

        public void Initialize(Parties paties)
        {
            this.Parties = paties;
            Broker.Global.Publish(StartBattle.Get());
            this.NextTurn();
        }

        public void NextTurn()
        {
            this.BehavioralOrder.Elapse(this.Parties);
            var order = this.BehavioralOrder.Simulation(this.Parties, Constants.TurnSimulationNumber);
            Broker.Global.Publish(Events.Battle.NextTurn.Get(order));
            Broker.Global.Publish(SelectCommand.Get(order[0]));
        }

        public void EndTurn(Character character)
        {
            this.BehavioralOrder.EndTurn(this.Parties);
            Broker.Global.Publish(Events.Battle.EndTurn.Get(character));

            // 何かしらイベントが登録された場合は実行開始
            if (this.EndTurnEvents.Count > 0)
            {
                this.InvokeEndTurnEvent();
            }
            // 何もない場合はバトル判定へ
            else
            {
                this.Judgement();
            }
        }

        private void InvokeEndTurnEvent()
        {
            Assert.AreNotEqual(this.EndTurnEvents.Count, 0, "ターン終了イベントがありません");
            this.EndTurnEvents.Dequeue()();
        }

        public void Judgement()
        {
            var battleResult = this.Parties.Result;
            if(battleResult == Constants.BattleResult.Unsettlement)
            {
                this.NextTurn();
            }
            else
            {
                Broker.Global.Publish(EndBattle.Get(battleResult));
            }
        }
    }
}
