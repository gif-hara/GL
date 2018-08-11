using GL.Battle;

namespace GL.Battle.CharacterControllers.StatusAilments
{
    /// <summary>
    /// 状態異常の孤軍奮闘を制御するクラス
    /// </summary>
    public sealed class Soldier : Element
    {
        /// <summary>
        /// 発動したか
        /// </summary>
        private bool canInvoke = true;
        
        public Soldier(int remainingTurn, Constants.StatusAilmentType type, CharacterAilmentController controller)
            : base(remainingTurn, type, controller)
        {
            this.Invoke();
        }

        public override void EndTurnAll(Character invoker)
        {
            base.EndTurnAll(invoker);
            this.Invoke();
        }

        public override void OnRemove()
        {
            base.OnRemove();
            if (!this.canInvoke)
            {
                this.controller.Character.StatusController.OnSoldier.Reset();
            }
        }

        private void Invoke()
        {
            if (!this.canInvoke)
            {
                return;
            }
            var character = this.controller.Character;
            var party = BattleManager.Instance.Parties.Ally(character);
            var survivals = party.SurvivalMembers;
            if (survivals.Find(c => c == character) != null && survivals.Count == 1)
            {
                var statusController = character.StatusController;
                statusController.OnSoldier.Copy(statusController.Base);
                this.canInvoke = false;
            }
        }
    }
}
