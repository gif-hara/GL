using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.CharacterControllers.StatusAilments
{
    /// <summary>
    /// 状態異常を制御するクラス
    /// </summary>
    /// <remarks>
    /// 特に継承する必要の無い状態異常はこのクラスを利用する
    /// </remarks>
    public class Element
    {
        public int RemainingTurn { get; private set; }

        public Constants.StatusAilmentType Type { get; private set; }

        protected readonly CharacterAilmentController controller;

        /// <summary>
        /// この状態異常が発動したターン
        /// </summary>
        private readonly int invokedTurnNumber;

        public Element(int remainingTurn, Constants.StatusAilmentType type, CharacterAilmentController controller)
        {
            this.RemainingTurn = remainingTurn;
            this.Type = type;
            this.controller = controller;
            this.invokedTurnNumber = BattleManager.Instance.TurnNumber;
        }

        public virtual void TakeDamage()
        {
        }

        public virtual void EndTurn()
        {
            // 発動したターンなら減算しない
            if (this.invokedTurnNumber == BattleManager.Instance.TurnNumber)
            {
                return;
            }
            
            this.RemainingTurn--;
            if (this.RemainingTurn <= 0)
            {
                this.controller.Elements.Remove(this);
            }
        }

        protected void ForceRemove()
        {
            this.controller.Elements.Remove(this);
        }
    }
}
