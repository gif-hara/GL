using System;
using System.Linq;
using GL.Battle.CharacterControllers;
using GL.Battle;
using HK.GL.Extensions;

namespace GL.Battle.Commands.Element.Implements
{
    /// <summary>
    /// 属性値上昇を行うコマンド.
    /// </summary>
    public sealed class AddAttribute : Implement<AddAttribute.Parameter>
    {
        public AddAttribute(Parameter parameter)
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
            var value = Calculator.GetAddAttributeValue(this.parameter.Rate);
            targets.ForEach(t =>
            {
                t.StatusController.AddAttributeToDynamic(this.parameter.AttributeType, value);
                if (bundle.CanRecord)
                {
                    BattleManager.Instance.InvokedCommandResult.AddAttributes.Add(new InvokedCommandResult.AddAttribute(t, this.parameter.AttributeType, value));
                }
            });
        }

        [Serializable]
        public class Parameter : IParameter
        {
            /// <summary>
            /// 加算する属性値のタイプ
            /// </summary>
            public Constants.AttributeType AttributeType;

            /// <summary>
            /// 倍率
            /// </summary>
            public float Rate;
        }
    }
}
