using UnityEngine;
using UnityEngine.Assertions;
using HK.Framework.EventSystems;
using HK.GL.Events.Battle;

namespace HK.GL.Battle
{
    /// <summary>
    /// バトルを管理するヤーツ.
    /// </summary>
    [RequireComponent(typeof(BehavioralOrderController))]
    public sealed class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { private set; get; }

        public BattleParty Party { private set; get; }

        public BehavioralOrderController BehavioralOrder { private set; get; }

        void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;

            this.BehavioralOrder = this.GetComponent<BehavioralOrderController>();
        }

        void OnDestroy()
        {
            Instance = null;
        }

        public void Initialize(BattleParty party)
        {
            this.Party = party;
            UniRxEvent.GlobalBroker.Publish(StartBattle.Get());
            this.NextTurn();
        }

        public void NextTurn()
        {
            this.BehavioralOrder.Elapse(this.Party);
            var order = this.BehavioralOrder.Simulation(this.Party, Constants.TurnSimulationNumber);
            UniRxEvent.GlobalBroker.Publish(Events.Battle.NextTurn.Get(order));
        }

        public void EndTurn()
        {
            this.BehavioralOrder.EndTurn(this.Party);

            var battleResult = this.Party.Result;
            if(battleResult == Constants.BattleResult.Unsettlement)
            {
                UniRxEvent.GlobalBroker.Publish(Events.Battle.EndTurn.Get());
                this.NextTurn();
            }
            else
            {
                UniRxEvent.GlobalBroker.Publish(EndBattle.Get(battleResult));
            }
        }
    }
}
