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

        /// <summary>
        /// ダメージを受けた際の処理
        /// </summary>
        public virtual void TakeDamage()
        {
        }

        /// <summary>
        /// ターン終了処理
        /// </summary>
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
                this.controller.Remove(this);
            }
        }

        /// <summary>
        /// 全体のターン終了処理
        /// </summary>
        /// <remarks>
        /// 全てのキャラクターのターン終了処理時に呼び出されます
        /// </remarks>
        public virtual void EndTurnAll()
        {
        }

        /// <summary>
        /// 削除される際の処理
        /// </summary>
        public virtual void OnRemove()
        {
        }

        /// <summary>
        /// 強制的に削除する
        /// </summary>
        protected void ForceRemove()
        {
            this.controller.Remove(this);
        }
    }
}
