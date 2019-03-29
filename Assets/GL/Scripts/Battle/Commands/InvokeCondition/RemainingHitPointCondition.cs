using System.Collections.Generic;
using System.Linq;
using GL.Battle.CharacterControllers;
using UnityEngine;
using UnityEngine.Assertions;
using static GL.Constants;

namespace GL.Battle
{
    /// <summary>
    /// 残りのヒットポイントからコマンドを実行できるか返すクラス
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/InvokeConditions/RemainingHitPoint")]
    public sealed class RemainingHitPointCondition : CommandInvokeCondition
    {
        [SerializeField]
        private TargetPartyType targetPartyType = TargetPartyType.Ally;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float percentage = 0.0f;

        [SerializeField]
        private ComparisonType comparisonType = ComparisonType.Greater;

        public override string Description
        {
            get
            {
                var percentage = this.percentage.ToString("P");

                // FIXME
                var comparisonType =
                    this.comparisonType == ComparisonType.Greater ? "より大きい"
                    : this.comparisonType == ComparisonType.Less ? "より小さい"
                    : "と同じ";

                // FIXME
                var targetPartyType = this.targetPartyType == TargetPartyType.Ally ? "味方の" : "敵に";

                return this.description.Format(percentage, comparisonType, targetPartyType);
            }
        }

        public override Character[] Suitable(BattleManager battleManager, Character invoker)
        {
            Assert.AreNotEqual(this.comparisonType, ComparisonType.Equal);

            var targetParties = battleManager.Parties.GetFromTargetPartyType(invoker, this.targetPartyType);
            var result = targetParties.SurvivalMembers.Where(this.CanAdd).ToList();
            result.Sort(this.Compare);

            return result.ToArray();
        }

        private bool CanAdd(Character character)
        {
            return this.comparisonType == ComparisonType.Greater
                ? character.StatusController.HitPointRate > this.percentage
                : character.StatusController.HitPointRate < this.percentage;
        }

        private int Compare(Character a, Character b)
        {
            return this.comparisonType == ComparisonType.Greater
                ? a.StatusController.HitPoint - b.StatusController.HitPoint
                : b.StatusController.HitPoint - a.StatusController.HitPoint;
        }
    }
}
