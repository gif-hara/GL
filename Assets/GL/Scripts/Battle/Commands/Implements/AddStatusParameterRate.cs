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

        public override Constants.CommandType CommandType
        {
            get
            {
                return this.parameter.Rate > 0.0f
                    ? Constants.CommandType.ParameterUp
                    : Constants.CommandType.ParameterDown;
            }
        }

        public override void Invoke(Character invoker)
        {
            base.Invoke(invoker);
            
            var targets = BattleManager.Instance.Parties
                .GetFromTargetPartyType(invoker, this.TargetPartyType)
                .GetTargets(invoker, this.TargetType, c => c.StatusController.GetTotalParameter(this.parameter.StatusParameterType));
            
            // 対象全てが死亡していた場合は何もしない
            if (targets.Find(t => !t.StatusController.IsDead) == null)
            {
                this.Postprocess(invoker);
                return;
            }

            invoker.StartAttack(() =>
            {
                var value = Calculator.GetAddStatusParameterValue(this.parameter.StatusParameterType, invoker.StatusController, this.parameter.Rate);
                targets.ForEach(t =>
                {
                    t.StatusController.AddParameterToDynamic(this.parameter.StatusParameterType, value);
                    if (this.CanRecord)
                    {
                        BattleManager.Instance.InvokedCommandResult.AddParameters.Add(new InvokedCommandResult.AddParameter(t, this.parameter.StatusParameterType, value));
                    }
                });
            }, this.Postprocess(invoker));
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
