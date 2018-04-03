using GL.Scripts.Battle.CharacterControllers;
using HK.GL.Battle;

namespace GL.Scripts.Battle.Command
{
    /// <summary>
    /// 攻撃を行うコマンド.
    /// </summary>
    public sealed class Command_Attack : ICommand
    {
        private string name;

        private Constants.TargetType targetType;

        /// <summary>
        /// ダメージ倍率
        /// </summary>
        private float rate;

        string ICommand.Name { get{ return this.name; } }

        Constants.TargetType ICommand.TargetType { get{ return this.targetType; } }

        void ICommand.Invoke(Character invoker)
        {
            invoker.StartAttack(() =>
            {
                var targets = BattleManager.Instance.Party
                    .Opponent(invoker)
                    .GetTargets(this.targetType, c => c.Status.HitPoint);
                targets.ForEach(t => t.TakeDamage(Calculator.GetBasicAttackDamage(invoker.Status, t.Status, this.rate)));
                BattleManager.Instance.EndTurn();
            });
        }

        public Command_Attack(string name, Constants.TargetType targetType, float rate)
        {
            this.name = name;
            this.targetType = targetType;
            this.rate = rate;
        }
    }
}
