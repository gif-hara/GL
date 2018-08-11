using GL.Battle.Systems;
using GL.Events.Battle;
using HK.Framework.EventSystems;

namespace GL.Battle.CharacterControllers.StatusAilments
{
    /// <summary>
    /// 状態異常の再生を制御するクラス
    /// </summary>
    public sealed class Regeneration : Element
    {
        public Regeneration(int remainingTurn, Constants.StatusAilmentType type, CharacterAilmentController controller)
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
            var character = this.controller.Character;
            character.Recovery(Calculator.GetRegenerationAmount(character.StatusController));
            Broker.Global.Publish(CompleteEndTurnEvent.Get());
        }
    }
}
