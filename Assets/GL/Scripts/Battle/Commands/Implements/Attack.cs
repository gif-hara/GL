using System;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.Commands.Implements
{
    /// <summary>
    /// 攻撃を行うコマンド.
    /// </summary>
    public sealed class Attack : Implement<Attack.Parameter>
    {
        public Attack(Parameter parameter)
            : base(parameter)
        {
        }

        public override Constants.CommandType CommandType { get { return Constants.CommandType.Attack; } }

        public override void Invoke(Character invoker)
        {
            base.Invoke(invoker);
            
            var targets = BattleManager.Instance.Parties
                .GetFromTargetPartyType(invoker, this.TargetPartyType)
                .GetTargets(invoker, this.TargetType, c => c.StatusController.GetTotalParameter(Constants.StatusParameterType.HitPoint));
            
            // 対象全てが死亡していた場合は何もしない
            if (targets.Find(t => !t.StatusController.IsDead) == null)
            {
                this.Postprocess(invoker)();
                return;
            }
            invoker.StartAttack(() =>
            {
                targets.ForEach(t =>
                {
                    var damage = Calculator.GetBasicAttackDamage(invoker, t, this.parameter.Rate);
                    t.TakeDamage(damage);

                    if (this.CanRecord)
                    {
                        BattleManager.Instance.InvokedCommandResult.TakeDamages.Add(new InvokedCommandResult.TakeDamage(t, damage));
                    }
                });
            }, this.Postprocess(invoker));
        }

        [Serializable]
        public class Parameter : CommandParameter
        {
            /// <summary>
            /// ダメージ倍率
            /// </summary>
            public float Rate;
        }
    }
}
