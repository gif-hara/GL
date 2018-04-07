using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.Commands.Impletents
{
    /// <summary>
    /// パラメータ倍率上昇を行うコマンド.
    /// </summary>
    public sealed class AddStatusParameterRate : Implement
    {
        private readonly Constants.StatusParameterType statusParameterType;

        private readonly float rate;
        
        public AddStatusParameterRate(
            string name,
            Constants.TargetPartyType targetPartyType,
            Constants.TargetType targetType,
            Constants.StatusParameterType statusParameterType,
            float rate
            )
            : base(name, targetPartyType, targetType)
        {
            this.statusParameterType = statusParameterType;
            this.rate = rate;
        }

        public override void Invoke(Character invoker)
        {
            invoker.StartAttack(() =>
            {
                var targets = BattleManager.Instance.Parties
                    .GetFromTargetPartyType(invoker, this.TargetPartyType)
                    .GetTargets(this.TargetType, c => c.StatusController.BaseStatus.Defense);
                var value = Calculator.GetAddStatusParameterValue(this.statusParameterType, invoker.StatusController, this.rate);
                targets.ForEach(t => t.StatusController.AddStatusParameter(this.statusParameterType, value));
            });
        }
    }
}
