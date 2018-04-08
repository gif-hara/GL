using System;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.Commands.Implements
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

        public override void Invoke(Character invoker)
        {
            invoker.StartAttack(() =>
            {
                var targets = BattleManager.Instance.Parties
                    .GetFromTargetPartyType(invoker, this.TargetPartyType)
                    .GetTargets(this.TargetType, c => c.StatusController.GetTotalParameter(this.parameter.StatusParameterType));
                var value = Calculator.GetAddStatusParameterValue(this.parameter.StatusParameterType, invoker.StatusController, this.parameter.Rate);
                targets.ForEach(t => t.StatusController.AddParameterToDynamic(this.parameter.StatusParameterType, value));
            });
        }

        [Serializable]
        public class Parameter : CommandParameter
        {
            /// <summary>
            /// 上昇させるパラメータのタイプ
            /// </summary>
            public Constants.StatusParameterType StatusParameterType;

            /// <summary>
            /// 倍率
            /// </summary>
            public float Rate;
        }
    }
}
