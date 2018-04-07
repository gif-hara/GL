using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.Commands.Impletents
{
    /// <summary>
    /// 素早さ倍率上昇を行うコマンド.
    /// </summary>
    public sealed class SpeedUpRate : Implement
    {
        public SpeedUpRate(string name, Constants.TargetPartyType targetPartyType, Constants.TargetType targetType)
            : base(name, targetPartyType, targetType)
        {
        }

        public override void Invoke(Character invoker)
        {
            invoker.StartAttack(() =>
            {
                var targets = BattleManager.Instance.Parties
                    .GetFromTargetPartyType(invoker, this.TargetPartyType)
                    .GetTargets(this.TargetType, c => c.StatusController.BaseStatus.Defense);
                var addDefense = Calculator.GetAddSpeedValue(invoker.StatusController);
                targets.ForEach(t => t.AddDefense(addDefense));
            });
        }
    }
}
