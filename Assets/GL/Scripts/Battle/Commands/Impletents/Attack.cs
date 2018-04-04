using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.Commands.Impletents
{
    /// <summary>
    /// 攻撃を行うコマンド.
    /// </summary>
    public sealed class Attack : IImplement
    {
        private string name;

        private Constants.TargetType targetType;

        /// <summary>
        /// ダメージ倍率
        /// </summary>
        private float rate;

        string IImplement.Name { get{ return this.name; } }

        Constants.TargetType IImplement.TargetType { get{ return this.targetType; } }

        void IImplement.Invoke(Character invoker)
        {
            invoker.StartAttack(() =>
            {
                var targets = BattleManager.Instance.Parties
                    .Opponent(invoker)
                    .GetTargets(this.targetType, c => c.StatusController.HitPoint);
                targets.ForEach(t => t.TakeDamage(Calculator.GetBasicAttackDamage(invoker.StatusController, t.StatusController, this.rate)));
                BattleManager.Instance.EndTurn();
            });
        }

        public Attack(string name, Constants.TargetType targetType, float rate)
        {
            this.name = name;
            this.targetType = targetType;
            this.rate = rate;
        }
    }
}
