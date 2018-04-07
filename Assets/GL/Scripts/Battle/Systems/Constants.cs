namespace GL.Scripts.Battle.Systems
{
    /// <summary>
    /// バトルで使用する定数.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// ターゲットタイプ
        /// </summary>
        public enum TargetType : int
        {
            /// <summary>
            /// 強いヤツ
            /// </summary>
            /// <remarks>
            /// 攻撃の場合はHPが高いキャラクター
            /// 攻撃力UP系援護なら攻撃力が高いキャラクターと言う具合に扱われる
            /// </remarks>
            Strong,

            /// <summary>
            /// 弱いヤツ
            /// </summary>
            /// <remarks>
            /// 攻撃の場合はHPが低いキャラクター
            /// 攻撃力UP系援護なら攻撃力が低いキャラクターと言う具合に扱われる
            /// </remarks>
            Weak,

            /// <summary>
            /// 対象のパーティ全て
            /// </summary>
            All,
        }

        /// <summary>
        /// キャラクタータイプ
        /// </summary>
        public enum CharacterType : int
        {
            /// <summary>
            /// プレイヤー
            /// </summary>
            Player,

            /// <summary>
            /// 敵
            /// </summary>
            Enemy,
        }

        /// <summary>
        /// 状態異常
        /// </summary>
        public enum StatusAilmentType
        {
            /// <summary>鬼神化</summary>
            /// <remarks>次のターンの与えるダメージが1.5倍になる</remarks>
            Demonization = 1,
            
            /// <summary>硬質化</summary>
            /// <remarks>次のターンの受けるダメージが0になる</remarks>
            Hardening = 2,
            
            /// <summary>鎌鼬</summary>
            /// <remarks>次のターンの攻撃が全体攻撃になる</remarks>
            Sickle = 3,
            
            /// <summary>先制</summary>
            /// <remarks>次に行動するキャラクターは<see cref="PreEmpt"/>を持つキャラクターになる</remarks>
            PreEmpt = 4,
            
            /// <summary>毒</summary>
            /// <remarks>ターン終了時に一定量のダメージを受ける</remarks>
            Poison = 101,
            
            /// <summary>麻痺</summary>
            /// <remarks>1ターン何も出来ない</remarks>
            Paralysis = 102,
        }

        /// <summary>
        /// バトル結果
        /// </summary>
        public enum BattleResult : int
        {
            /// <summary>
            /// 未決着
            /// </summary>
            Unsettlement,

            /// <summary>
            /// プレイヤーの勝利
            /// </summary>
            PlayerWin,

            /// <summary>
            /// 敵の勝利
            /// </summary>
            EnemyWin,
        }

        public const int TurnSimulationNumber = 10;
    }
}
