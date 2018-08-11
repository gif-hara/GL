using System;
using GL.Battle.Systems;
using GL.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;

namespace GL.Battle.CharacterControllers.StatusAilments
{
    /// <summary>
    /// 状態異常の麻痺を制御するクラス
    /// </summary>
    public sealed class Paralysis : Element
    {
        public Paralysis(int remainingTurn, Constants.StatusAilmentType type, CharacterAilmentController controller)
            : base(remainingTurn, type, controller)
        {
        }

        public override void EndTurn()
        {
            BattleManager.Instance.EndTurnEvents.Enqueue(this.OnEndTurnEvent);
            base.EndTurn();
        }

        private void OnEndTurnEvent()
        {
            // 同フレーム内でターン経過処理を行うとイベントの流れが正しく無くなるので遅らせる
            Observable.Timer(TimeSpan.FromSeconds(1.0f))
                .Subscribe(_ => Broker.Global.Publish(CompleteEndTurnEvent.Get()));
        }
    }
}
