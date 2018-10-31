using System;
using System.Linq;
using GL.Battle.CharacterControllers;
using GL.Battle;
using HK.GL.Extensions;

namespace GL.Battle.Commands.Element.Implements
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
                var damage = Calculator.GetBasicAttackDamage(invoker, t, this.parameter.Rate, this.parameter.AttributeType);

                // ダメージがマイナスの場合は回復扱いになる
                if(damage < 0)
                {
                    t.Recovery(-damage);
                }
                else
                {
                    t.TakeDamage(damage);
                }

                if (bundle.CanRecord)
                {
                    BattleManager.Instance.InvokedCommandResult.TakeDamages.Add(new InvokedCommandResult.TakeDamage(t, damage));
                }
            });
        }

        [Serializable]
        public class Parameter : IParameter
        {
            /// <summary>
            /// ダメージ倍率
            /// </summary>
            public float Rate;

            /// <summary>
            /// 属性
            /// </summary>
            public Constants.AttributeType AttributeType;

            /// <summary>
            /// 攻撃ロジック
            /// </summary>
            public Constants.AttackLogicType LogicType;
        }
    }
}
