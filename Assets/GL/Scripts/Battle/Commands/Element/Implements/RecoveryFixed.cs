using System;
using System.Linq;
using GL.Battle.CharacterControllers;
using GL.Battle;
using HK.GL.Extensions;

namespace GL.Battle.Commands.Element.Implements
{
    /// <summary>
    /// 固定値回復を行うコマンド.
    /// </summary>
    public sealed class RecoveryFixed : Implement<RecoveryFixed.Parameter>
    {
        public RecoveryFixed(Parameter parameter)
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
                t.Recovery(this.parameter.Value);
            });
        }

        [Serializable]
        public class Parameter : IParameter
        {
            /// <summary>
            /// 回復値
            /// </summary>
            public int Value;
        }
    }
}
