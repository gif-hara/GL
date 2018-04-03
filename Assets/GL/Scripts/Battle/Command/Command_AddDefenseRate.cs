using GL.Scripts.Battle.CharacterControllers;
using HK.GL.Battle;

namespace GL.Scripts.Battle.Command
{
    /// <summary>
    /// 防御力倍率上昇を行うコマンド.
    /// </summary>
    public sealed class Command_AddDefenseRate : ICommand
    {
        private string name;

        private Constants.TargetType targetType;

        string ICommand.Name { get{ return this.name; } }

        Constants.TargetType ICommand.TargetType { get{ return this.targetType; } }

        void ICommand.Invoke(Character invoker)
        {
            invoker.StartAttack(() =>
            {
                var targets = BattleManager.Instance.Party
                    .Ally(invoker)
                    .GetTargets(this.targetType, c => c.Status.Defense);
                var addDefense = Calculator.GetAddDefenseValue(invoker.Status);
                targets.ForEach(t => t.AddDefense(addDefense));
                BattleManager.Instance.EndTurn();
            });
        }

        public Command_AddDefenseRate(string name, Constants.TargetType targetType)
        {
            this.name = name;
            this.targetType = targetType;
        }
    }
}
