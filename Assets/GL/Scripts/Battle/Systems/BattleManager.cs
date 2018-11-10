using System;
using System.Collections.Generic;
using GL.Battle.CharacterControllers;
using GL.Battle.PartyControllers;
using GL.Database;
using GL.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace GL.Battle
{
    /// <summary>
    /// バトルを管理するヤーツ.
    /// </summary>
    [RequireComponent(typeof(BehavioralOrderController))]
    public sealed class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { private set; get; }

        public Commands.Bundle.Implement ConfuseCommand { get; private set; }

        public Commands.Bundle.Implement BerserkCommand { get; private set; }

        public Commands.Bundle.Implement ChaseCommand { get; private set; }

        public Commands.Bundle.Implement CounterCommand { get; private set; }

        public Parties Parties { private set; get; }

        public BehavioralOrderController BehavioralOrder { private set; get; }
        
        public readonly Queue<Action> EndTurnEvents = new Queue<Action>();

        public int TurnNumber { get; private set; }

        public readonly InvokedCommandResult InvokedCommandResult = new InvokedCommandResult();

        public BattleAcquireElementController AcquireElementController { get; private set; }

        void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;

            var constantCommand = MasterData.ConstantCommand;
            this.ConfuseCommand = constantCommand.Confuse.Create();
            this.BerserkCommand = constantCommand.Berserk.Create();
            this.ChaseCommand = constantCommand.Chase.Create();
            this.CounterCommand = constantCommand.Counter.Create();

            this.BehavioralOrder = this.GetComponent<BehavioralOrderController>();
            new CommandInvoker(this.gameObject);
            this.AcquireElementController = new BattleAcquireElementController(this.gameObject);

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
            Broker.Global.Publish(GL.Events.Battle.NextTurn.Get(order));
            Broker.Global.Publish(StartSelectCommand.Get(order[0]));
        }

        public void EndTurn(Character character)
        {
            this.BehavioralOrder.EndTurn(this.Parties);
            Broker.Global.Publish(GL.Events.Battle.EndTurn.Get(character));

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

        public void ToHomeScene()
        {
            SceneManager.LoadScene("Home");
        }
    }
}
