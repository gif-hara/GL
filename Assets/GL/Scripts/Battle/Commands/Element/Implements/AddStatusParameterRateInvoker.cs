using System;
using System.Linq;
using GL.Battle.CharacterControllers;
using GL.Battle;
using HK.GL.Extensions;

namespace GL.Battle.Commands.Element.Implements
{
    /// <summary>
    /// コマンド実行者に対してパラメータ倍率上昇を行うコマンド.
    /// </summary>
    public sealed class AddStatusParameterRateInvoker : Implement<AddStatusParameterRateInvoker.Parameter>
    {
        public AddStatusParameterRateInvoker(Parameter parameter)
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
            var value = Calculator.GetAddStatusParameterValue(this.parameter.ParameterType, invoker.StatusController, this.parameter.Rate);
            invoker.StatusController.AddParameterToDynamic(this.parameter.ParameterType, value);
            if (bundle.CanRecord)
            {
                BattleManager.Instance.InvokedCommandResult.AddParameters.Add(new InvokedCommandResult.AddParameter(invoker, this.parameter.ParameterType, value));
            }
        }

        [Serializable]
        public class Parameter : IParameter
        {
            /// <summary>
            /// 加算するパラメータのタイプ
            /// </summary>
            public Constants.StatusParameterType ParameterType;

            /// <summary>
            /// 倍率
            /// </summary>
            public float Rate;
        }
    }
}
