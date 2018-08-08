using System;
using System.Linq;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using HK.GL.Extensions;

namespace GL.Scripts.Battle.Commands.Element.Implements
{
    /// <summary>
    /// パラメータ倍率上昇を行うコマンド.
    /// </summary>
    public sealed class AddStatusParameterRate : Implement<AddStatusParameterRate.Parameter>
    {
        public AddStatusParameterRate(Parameter parameter)
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
            targets.ForEach(t =>
            {
                t.StatusController.AddParameterToDynamic(this.parameter.ParameterType, value);
                if (bundle.CanRecord)
                {
                    BattleManager.Instance.InvokedCommandResult.AddParameters.Add(new InvokedCommandResult.AddParameter(t, this.parameter.ParameterType, value));
                }
            });
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
