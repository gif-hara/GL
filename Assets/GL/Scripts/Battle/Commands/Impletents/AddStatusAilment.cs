using System;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using Random = UnityEngine.Random;

namespace GL.Scripts.Battle.Commands.Impletents
{
    /// <summary>
    /// 状態異常を追加するコマンド.
    /// </summary>
    public sealed class AddStatusAilment : Implement<AddStatusAilment.AddStatusAilmentParameter>
    {
        public AddStatusAilment(AddStatusAilmentParameter parameter)
            : base(parameter)
        {
        }

        public override void Invoke(Character invoker)
        {
            invoker.StartAttack(() =>
            {
                var targets = BattleManager.Instance.Parties
                    .GetFromTargetPartyType(invoker, this.TargetPartyType)
                    .GetTargets(this.TargetType, c => c.StatusController.BaseStatus.HitPoint);
                targets.ForEach(t =>
                {
                    if (Calculator.LotteryStatusAilment(this.Parameter.Rate))
                    {
                        t.AilmentController.Add(this.RemainingTurn, this.Parameter.StatusAilmentType);
                    }
                });
            });
        }

        private int RemainingTurn
        {
            get { return Random.Range(this.Parameter.RemainingTurnMin, this.Parameter.RemainingTurnMax + 1); }
        }

        [Serializable]
        public class AddStatusAilmentParameter : CommandParameter
        {
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
