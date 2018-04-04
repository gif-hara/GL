using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.Commands.Impletents
{
    /// <summary>
    /// 攻撃力倍率上昇を行うコマンド.
    /// </summary>
    public sealed class StrengthUpRate : Implement
    {
        public StrengthUpRate(string name, Constants.TargetType targetType)
            : base(name, targetType)
        {
        }

        public override void Invoke(Character invoker)
        {
            invoker.StartAttack(() =>
            {
                var targets = BattleManager.Instance.Parties
                    .Ally(invoker)
                    .GetTargets(this.TargetType, c => c.StatusController.BaseStatus.Defense);
                var addDefense = Calculator.GetAddStrengthValue(invoker.StatusController);
                targets.ForEach(t => t.AddStrength(addDefense));
                BattleManager.Instance.EndTurn();
            });
        }
    }
}
