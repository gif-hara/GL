using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.Commands.Impletents
{
    /// <summary>
    /// 攻撃力倍率上昇を行うコマンド.
    /// </summary>
    public sealed class StrengthUpRate : IImplement
    {
        private string name;

        private Constants.TargetType targetType;

        string IImplement.Name { get{ return this.name; } }

        Constants.TargetType IImplement.TargetType { get{ return this.targetType; } }

        void IImplement.Invoke(Character invoker)
        {
            invoker.StartAttack(() =>
            {
                var targets = BattleManager.Instance.Parties
                    .Ally(invoker)
                    .GetTargets(this.targetType, c => c.StatusController.BaseStatus.Defense);
                var addDefense = Calculator.GetAddStrengthValue(invoker.StatusController);
                targets.ForEach(t => t.AddStrength(addDefense));
                BattleManager.Instance.EndTurn();
            });
        }

        public StrengthUpRate(string name, Constants.TargetType targetType)
        {
            this.name = name;
            this.targetType = targetType;
        }
    }
}
