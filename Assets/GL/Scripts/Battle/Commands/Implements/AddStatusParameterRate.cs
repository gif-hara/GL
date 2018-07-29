using System;
using System.Linq;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using HK.GL.Extensions;

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
            
            // 対象全てが死亡していた場合は何もしない
            if (!targets.Any())
            {
                this.Postprocess(invoker);
                return;
            }

            invoker.StartAttack(() =>
            {
                var value = Calculator.GetAddStatusParameterValue(this.TargetStatusParameterType, invoker.StatusController, this.parameter.Rate);
                targets.ForEach(t =>
                {
                    t.StatusController.AddParameterToDynamic(this.TargetStatusParameterType, value);
                    if (this.CanRecord)
                    {
                        BattleManager.Instance.InvokedCommandResult.AddParameters.Add(new InvokedCommandResult.AddParameter(t, this.TargetStatusParameterType, value));
                    }
                });
            }, this.Postprocess(invoker));
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
