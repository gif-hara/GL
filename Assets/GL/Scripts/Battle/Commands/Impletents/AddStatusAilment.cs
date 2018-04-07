using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using UnityEngine;

namespace GL.Scripts.Battle.Commands.Impletents
{
    /// <summary>
    /// 状態異常を追加するコマンド.
    /// </summary>
    public sealed class AddStatusAilment : Implement
    {
        private Constants.StatusAilmentType type;

        /// <summary>
        /// 状態異常にかかる確率
        /// </summary>
        private float rate;
        
        private int remainingTurnMin;

        private int remainingTurnMax;
        
        public AddStatusAilment(
            string name,
            Constants.TargetPartyType targetPartyType,
            Constants.TargetType targetType,
            Constants.StatusAilmentType type,
            float rate,
            int remainingMin,
            int remainingMax
            )
            : base(name, targetPartyType, targetType)
        {
            this.type = type;
            this.rate = rate;
            this.remainingTurnMin = remainingMin;
            this.remainingTurnMax = remainingMax;
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
                    if (Calculator.LotteryStatusAilment(this.rate))
                    {
                        t.AilmentController.Add(this.RemainingTurn, this.type);
                    }
                });
            });
        }

        private int RemainingTurn
        {
            get { return Random.Range(this.remainingTurnMin, this.remainingTurnMax + 1); }
        }
    }
}
