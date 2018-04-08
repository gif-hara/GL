using GL.Scripts.Battle.Systems;
using GL.Scripts.Events.Battle;
using HK.Framework.EventSystems;

namespace GL.Scripts.Battle.CharacterControllers.StatusAilments
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
            character.TakeDamage(Calculator.GetPoisonDamage(character.StatusController));
            Broker.Global.Publish(CompleteEndTurnEvent.Get());
        }
    }
}
