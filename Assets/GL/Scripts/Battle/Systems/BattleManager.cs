using GL.Scripts.Battle.CharacterControllers;
using HK.Framework.EventSystems;
using HK.GL.Events.Battle;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Scripts.Battle.Systems
{
    /// <summary>
    /// バトルを管理するヤーツ.
    /// </summary>
    [RequireComponent(typeof(BehavioralOrderController))]
    public sealed class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { private set; get; }

        public Parties Parties { private set; get; }

        public BehavioralOrderController BehavioralOrder { private set; get; }

        void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;

            this.BehavioralOrder = this.GetComponent<BehavioralOrderController>();
            
            // FIXME: リザルト実装したら削除する
            Broker.Global.Receive<EndBattle>()
                .Take(1)
                .Subscribe(x => Debug.Log(x.Result))
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
            Broker.Global.Publish(HK.GL.Events.Battle.NextTurn.Get(order));
        }

        public void EndTurn()
        {
            this.BehavioralOrder.EndTurn(this.Parties);

            var battleResult = this.Parties.Result;
            if(battleResult == Constants.BattleResult.Unsettlement)
            {
                Broker.Global.Publish(HK.GL.Events.Battle.EndTurn.Get());
                this.NextTurn();
            }
            else
            {
                Broker.Global.Publish(EndBattle.Get(battleResult));
            }
        }
    }
}
