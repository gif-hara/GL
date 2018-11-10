using GL.Battle;
using GL.Events.Battle;
using HK.Framework.EventSystems;

namespace GL.Battle.CharacterControllers.StatusAilments
{
    /// <summary>
    /// 状態異常の仕返を制御するクラス
    /// </summary>
    public sealed class Counter : Element
    {
        public Counter(int remainingTurn, Constants.StatusAilmentType type, CharacterAilmentController controller)
            : base(remainingTurn, type, controller)
        {
        }

        public override void EndTurnAll(Character invoker)
        {
            base.EndTurnAll(invoker);

            // 自分自身が行動した場合は仕返ししない
            if (this.controller.Character == invoker)
            {
                return;
            }

            // 味方の攻撃は仕返ししない
            if (this.controller.Character.CharacterType == invoker.CharacterType)
            {
                return;
            }

            var invokedCommandResult = BattleManager.Instance.InvokedCommandResult;
            if (invokedCommandResult.TakeDamages.FindIndex(t => t.Target == this.controller.Character) == -1)
            {
                return;
            }

            BattleManager.Instance.EndTurnEvents.Enqueue(() =>
            {
                Broker.Global.Publish(SelectedCommand.Get(this.controller.Character, BattleManager.Instance.CounterCommand));
            });
        }
    }
}
