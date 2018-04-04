using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.Commands.Impletents
{
    /// <summary>
    /// 防御力倍率上昇を行うコマンド.
    /// </summary>
    public sealed class DefenseUpRate : IImplement
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
                var addDefense = Calculator.GetAddDefenseValue(invoker.StatusController);
                targets.ForEach(t => t.AddDefense(addDefense));
                BattleManager.Instance.EndTurn();
            });
        }

        public DefenseUpRate(string name, Constants.TargetType targetType)
        {
            this.name = name;
            this.targetType = targetType;
        }
    }
}
