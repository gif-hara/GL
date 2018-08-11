using GL.Battle;
using GL.Events.Battle;
using HK.Framework.EventSystems;

namespace GL.Battle.CharacterControllers.StatusAilments
{
    /// <summary>
    /// 状態異常の追い打ちを制御するクラス
    /// </summary>
    public sealed class Chase : Element
    {
        public Chase(int remainingTurn, Constants.StatusAilmentType type, CharacterAilmentController controller)
            : base(remainingTurn, type, controller)
        {
        }

        public override void EndTurnAll(Character invoker)
        {
            base.EndTurnAll(invoker);

            // 自分自身が行動した場合は追い打ちしない
            if (this.controller.Character == invoker)
            {
                return;
            }

            // 相手が攻撃した場合は何もしない
            if (this.controller.Character.CharacterType != invoker.CharacterType)
            {
                return;
            }

            var invokedCommandResult = BattleManager.Instance.InvokedCommandResult;
            if (invokedCommandResult.TakeDamages.Count != 1)
            {
                return;
            }
            
            BattleManager.Instance.EndTurnEvents.Enqueue(() =>
            {
                Broker.Global.Publish(SelectedCommand.Get(this.controller.Character, BattleManager.Instance.ChaseCommand));
            });
        }
    }
}
