using GL.Scripts.Battle.CharacterControllers;
using HK.GL.Battle;

namespace GL.Scripts.Battle.Commands.Impletents
{
    /// <summary>
    /// 防御力倍率上昇を行うコマンド.
    /// </summary>
    public sealed class AddDefenseRate : IImplement
    {
        private string name;

        private Constants.TargetType targetType;

        string IImplement.Name { get{ return this.name; } }

        Constants.TargetType IImplement.TargetType { get{ return this.targetType; } }

        void IImplement.Invoke(Character invoker)
        {
            invoker.StartAttack(() =>
            {
                var targets = BattleManager.Instance.Parties
                    .Ally(invoker)
                    .GetTargets(this.targetType, c => c.Status.Defense);
                var addDefense = Calculator.GetAddDefenseValue(invoker.Status);
                targets.ForEach(t => t.AddDefense(addDefense));
                BattleManager.Instance.EndTurn();
            });
        }

        public AddDefenseRate(string name, Constants.TargetType targetType)
        {
            this.name = name;
            this.targetType = targetType;
        }
    }
}
