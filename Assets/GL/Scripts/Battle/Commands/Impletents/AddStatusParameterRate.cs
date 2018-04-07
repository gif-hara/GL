using System;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.Commands.Impletents
{
    /// <summary>
    /// パラメータ倍率上昇を行うコマンド.
    /// </summary>
    public sealed class AddStatusParameterRate : Implement<AddStatusParameterRate.AddStatusParameterRateParameter>
    {
        public AddStatusParameterRate(AddStatusParameterRateParameter parameter)
            : base(parameter)
        {
        }

        public override void Invoke(Character invoker)
        {
            invoker.StartAttack(() =>
            {
                var targets = BattleManager.Instance.Parties
                    .GetFromTargetPartyType(invoker, this.TargetPartyType)
                    .GetTargets(this.TargetType, c => c.StatusController.BaseStatus.Defense);
                var value = Calculator.GetAddStatusParameterValue(this.Parameter.StatusParameterType, invoker.StatusController, this.Parameter.Rate);
                targets.ForEach(t => t.StatusController.AddStatusParameter(this.Parameter.StatusParameterType, value));
            });
        }

        [Serializable]
        public class AddStatusParameterRateParameter : CommandParameter
        {
            public Constants.StatusParameterType StatusParameterType;

            public float Rate;
        }
    }
}
