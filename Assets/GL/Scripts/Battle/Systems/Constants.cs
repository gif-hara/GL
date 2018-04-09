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
            
            /// <summary>
            /// 誰を狙うか分からない
            /// </summary>
            Random,
            
            /// <summary>
            /// 自分自身
            /// </summary>
            Myself,
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
        /// 対象となるパーティタイプ
        /// </summary>
        public enum TargetPartyType
        {
            /// <summary>
            /// 味方
            /// </summary>
            Ally,
            
            /// <summary>
            /// 相手
            /// </summary>
            Opponent,
        }
        
        /// <summary>
        /// パラメータ
        /// </summary>
        public enum StatusParameterType
        {
            /// <summary>ヒットポイント</summary>
            HitPoint,
            
            /// <summary>攻撃力</summary>
            Strength,
            
            /// <summary>防御力</summary>
            Defense,
            
            /// <summary>思いやり力</summary>
            Sympathy,
            
            /// <summary>ネガキャン力</summary>
            Nega,
            
            /// <summary>素早さ</summary>
            Speed,
        }

        /// <summary>
        /// 状態異常
        /// </summary>
        public enum StatusAilmentType
        {
            /// <summary>鬼神化</summary>
            /// <remarks>与えるダメージが1.5倍になるが受けるダメージも1.5倍になる</remarks>
            Demonization = 1,
            
            /// <summary>硬質化</summary>
            /// <remarks>受けるダメージが半分になる</remarks>
            Hardening = 2,
            
            /// <summary>鎌鼬</summary>
            /// <remarks>通常攻撃が全体攻撃になる</remarks>
            Sickle = 3,
            
            /// <summary>先制</summary>
            /// <remarks>次に行動するキャラクターは<see cref="PreEmpt"/>を持つキャラクターになる</remarks>
            PreEmpt = 4,
            
            /// <summary>再生</summary>
            /// <remarks>ターン終了時に一定量回復する</remarks>
            Regeneration = 5,
            
            /// <summary>孤軍奮闘</summary>
            /// <remarks>パーティが自分自身のみの場合全てのパラメータが倍になる</remarks>
            Soldier = 6,
            
            /// <summary>追い打ち</summary>
            /// <remarks>味方が攻撃した際に自分自身も通常攻撃を行う</remarks>
            Chase = 7,
            
            /// <summary>憤怒</summary>
            /// <remarks>攻撃を受ける度に攻撃力が上昇する</remarks>
            Rage = 8,
            
            /// <summary>庇う</summary>
            /// <remarks>味方が攻撃を受ける時、自分自身が身代わりになる</remarks>
            Protect = 9,
            
            /// <summary>毒</summary>
            /// <remarks>ターン終了時に一定量のダメージを受ける</remarks>
            Poison = 101,
            
            /// <summary>麻痺</summary>
            /// <remarks>このターン何も出来ない</remarks>
            Paralysis = 102,
            
            /// <summary>睡眠</summary>
            /// <remarks>ダメージを受けるまで何も出来ない</remarks>
            Sleep = 103,
            
            /// <summary>混乱</summary>
            /// <remarks>コマンドを選択できず、味方を攻撃してしまう</remarks>
            Confuse = 104,
            
            /// <summary>狂暴</summary>
            /// <remarks>コマンドを選択できず、通常攻撃のみ行う</remarks>
            Berserk = 105,
            
            /// <summary>急所</summary>
            /// <remarks>受けるダメージが必ずクリティカルになるが一度受けると解除される</remarks>
            Vitals = 106,
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
