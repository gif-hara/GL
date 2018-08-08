using System;
using System.Linq;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using HK.GL.Extensions;

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

        public override bool TakeDamage
        {
            get
            {
                return true;
            }
        }

        public override void Invoke(Character invoker, Commands.Bundle.Implement bundle, Character[] targets)
        {
            targets.ForEach(t =>
            {
                var damage = Calculator.GetBasicAttackDamage(invoker, t, this.parameter.Rate);
                t.TakeDamage(damage);

                if (bundle.CanRecord)
                {
                    BattleManager.Instance.InvokedCommandResult.TakeDamages.Add(new InvokedCommandResult.TakeDamage(t, damage));
                }
            });
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
