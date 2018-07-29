using System;
using System.Linq;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using HK.GL.Extensions;

namespace GL.Scripts.Battle.Commands.Implements
{
    /// <summary>
    /// 回復を行うコマンド.
    /// </summary>
    public sealed class Recovery : Implement<Recovery.Parameter>
    {
        public Recovery(Parameter parameter)
            : base(parameter)
        {
        }

        public override Constants.CommandType CommandType { get { return Constants.CommandType.Recovery; } }

        public override bool TakeDamage
        {
            get
            {
                return false;
            }
        }

        public override void Invoke(Character invoker, Character[] targets)
        {
            base.Invoke(invoker, targets);
            targets.ForEach(t =>
            {
                var amount = Calculator.GetRecoveryAmount(invoker, this.parameter.Rate);
                t.Recovery(amount);
            });
        }

        [Serializable]
        public class Parameter : CommandParameter
        {
            /// <summary>
            /// 倍率
            /// </summary>
            public float Rate;
        }
    }
}
