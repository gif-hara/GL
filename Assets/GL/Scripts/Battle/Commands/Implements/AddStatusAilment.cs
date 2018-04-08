using System;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
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

        public override void Invoke(Character invoker)
        {
            invoker.StartAttack(() =>
            {
                var targets = BattleManager.Instance.Parties
                    .GetFromTargetPartyType(invoker, this.TargetPartyType)
                    .GetTargets(this.TargetType, c => c.StatusController.GetTotalStatusParameter(this.parameter.TargetStatusParameterType));
                targets.ForEach(t =>
                {
                    if (Calculator.LotteryStatusAilment(t.StatusController, this.parameter.StatusAilmentType, this.parameter.Rate))
                    {
                        t.AilmentController.Add(this.RemainingTurn, this.parameter.StatusAilmentType);
                    }
                });
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
            /// かける対象を選ぶためのステータスパラメータタイプ
            /// </summary>
            public Constants.StatusParameterType TargetStatusParameterType;
            
            /// <summary>
            /// 状態異常にかかる確率
            /// </summary>
            public float Rate;
        
            public int RemainingTurnMin;

            public int RemainingTurnMax;
        }
    }
}
