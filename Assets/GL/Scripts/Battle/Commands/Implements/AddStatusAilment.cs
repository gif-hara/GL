using System;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using GL.Scripts.Extensions;
using Random = UnityEngine.Random;

namespace GL.Scripts.Battle.Commands.Implements
{
    /// <summary>
    /// 状態異常を追加するコマンド.
    /// </summary>
    public sealed class AddStatusAilment : Implement<AddStatusAilment.Parameter>
    {
        public AddStatusAilment(Parameter parameter)
            : base(parameter)
        {
        }

        public override Constants.CommandType CommandType
        {
            get
            {
                return this.parameter.StatusAilmentType.IsPositive()
                        ? Constants.CommandType.Buff
                        : Constants.CommandType.Debuff;
            }
        }

        public override void Invoke(Character invoker)
        {
            base.Invoke(invoker);

            var targets = this.GetTargets(invoker, false);
            
            // 対象全てが死亡していた場合は何もしない
            if (targets.Find(t => !t.StatusController.IsDead) == null)
            {
                this.Postprocess(invoker);
                return;
            }

            invoker.StartAttack(() =>
            {
                targets.ForEach(t =>
                {
                    if (Calculator.LotteryStatusAilment(t.StatusController, this.parameter.StatusAilmentType, this.parameter.Rate))
                    {
                        var success = t.AilmentController.Add(this.RemainingTurn, this.parameter.StatusAilmentType);
                        if (success && this.CanRecord)
                        {
                            BattleManager.Instance.InvokedCommandResult.AddAilments.Add(new InvokedCommandResult.AddAilment(t, this.parameter.StatusAilmentType));
                        }
                    }
                });
            }, this.Postprocess(invoker));
        }

        private int RemainingTurn
        {
            get { return Random.Range(this.parameter.RemainingTurnMin, this.parameter.RemainingTurnMax + 1); }
        }

        [Serializable]
        public class Parameter : CommandParameter
        {
            /// <summary>
            /// かける状態異常タイプ
            /// </summary>
            public Constants.StatusAilmentType StatusAilmentType;

            /// <summary>
            /// 状態異常にかかる確率
            /// </summary>
            public float Rate;
        
            public int RemainingTurnMin;

            public int RemainingTurnMax;
        }
    }
}
