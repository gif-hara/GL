using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.Commands.Impletents
{
    /// <summary>
    /// 攻撃を行うコマンド.
    /// </summary>
    public sealed class Attack : Implement
    {
        /// <summary>
        /// ダメージ倍率
        /// </summary>
        private float rate;

        public Attack(string name, Constants.TargetType targetType, float rate)
            : base(name, targetType)
        {
            this.rate = rate;
        }

        public override void Invoke(Character invoker)
        {
            invoker.StartAttack(() =>
            {
                var targets = BattleManager.Instance.Parties
                    .Opponent(invoker)
                    .GetTargets(this.TargetType, c => c.StatusController.BaseStatus.HitPoint);
                targets.ForEach(t => t.TakeDamage(Calculator.GetBasicAttackDamage(invoker.StatusController, t.StatusController, this.rate)));
            });
        }
    }
}
