using System;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.Commands.Impletents
{
    /// <summary>
    /// 攻撃を行うコマンド.
    /// </summary>
    public sealed class Attack : Implement<Attack.AttackParameter>
    {
        public Attack(AttackParameter parameter)
            : base(parameter)
        {
        }

        public override void Invoke(Character invoker)
        {
            invoker.StartAttack(() =>
            {
                var targets = BattleManager.Instance.Parties
                    .GetFromTargetPartyType(invoker, this.TargetPartyType)
                    .GetTargets(this.TargetType, c => c.StatusController.BaseStatus.HitPoint);
                targets.ForEach(t => t.TakeDamage(Calculator.GetBasicAttackDamage(invoker.StatusController, t.StatusController, this.Parameter.Rate)));
            });
        }

        [Serializable]
        public class AttackParameter : CommandParameter
        {
            /// <summary>
            /// ダメージ倍率
            /// </summary>
            public float Rate;
        }
    }
}
