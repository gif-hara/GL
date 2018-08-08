using System;
using System.Linq;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using GL.Scripts.Extensions;
using HK.GL.Extensions;
using Random = UnityEngine.Random;

namespace GL.Scripts.Battle.Commands.Element.Implements
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
                if (Calculator.LotteryStatusAilment(t.StatusController, this.parameter.StatusAilmentType, this.parameter.Rate))
                {
                    var success = t.AilmentController.Add(this.RemainingTurn, this.parameter.StatusAilmentType);
                    if (success && bundle.CanRecord)
                    {
                        BattleManager.Instance.InvokedCommandResult.AddAilments.Add(new InvokedCommandResult.AddAilment(t, this.parameter.StatusAilmentType));
                    }
                }
            });
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
