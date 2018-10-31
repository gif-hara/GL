using GL.Battle;
using GL.Events.Battle;
using HK.Framework.EventSystems;

namespace GL.Battle.CharacterControllers.StatusAilments
{
    /// <summary>
    /// 状態異常の毒を制御するクラス
    /// </summary>
    public sealed class Poison : Element
    {
        public Poison(int remainingTurn, Constants.StatusAilmentType type, CharacterAilmentController controller)
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
            character.TakeDamage(Calculator.GetPoisonDamage(character.StatusController), true);
            Broker.Global.Publish(CompleteEndTurnEvent.Get());
        }
    }
}
