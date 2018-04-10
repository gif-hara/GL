using System;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

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

        public override void Invoke(Character invoker)
        {
            base.Invoke(invoker);
            
            var targets = this.GetTargets(invoker, false);
            
            // 対象全てが死亡していた場合は何もしない
            if (targets.Find(t => !t.StatusController.IsDead) == null)
            {
                this.Postprocess(invoker)();
                return;
            }
            invoker.StartAttack(() =>
            {
                targets.ForEach(t =>
                {
                    var amount = Calculator.GetRecoveryAmount(invoker, this.parameter.Rate);
                    t.Recovery(amount);
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
