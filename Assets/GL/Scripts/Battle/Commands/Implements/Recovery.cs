using System;
using System.Linq;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using HK.GL.Extensions;

namespace GL.Scripts.Battle.Commands.Element.Implements
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

        public override bool TakeDamage
        {
            get
            {
                return false;
            }
        }

        public override void Invoke(Character invoker, Commands.Bundle.Implement bundle, Character[] targets)
        {
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
