using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.Commands.Impletents
{
    /// <summary>
    /// 防御力倍率上昇を行うコマンド.
    /// </summary>
    public sealed class DefenseUpRate : Implement
    {
        public DefenseUpRate(string name, Constants.TargetType targetType)
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
                var addDefense = Calculator.GetAddDefenseValue(invoker.StatusController);
                targets.ForEach(t => t.AddDefense(addDefense));
            });
        }
    }
}
