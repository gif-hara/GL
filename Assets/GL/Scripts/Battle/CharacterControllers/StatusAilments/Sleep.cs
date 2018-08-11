using System;
using GL.Battle.Systems;
using GL.Scripts.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;

namespace GL.Battle.CharacterControllers.StatusAilments
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Sleep : Element
    {
        public Sleep(int remainingTurn, Constants.StatusAilmentType type, CharacterAilmentController controller)
            : base(remainingTurn, type, controller)
        {
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            this.ForceRemove();
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
