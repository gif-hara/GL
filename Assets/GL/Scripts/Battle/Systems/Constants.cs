﻿using System;

namespace GL.Battle
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
            /// ターゲットを選択可能
            /// </summary>
            Select,

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
            
            /// <summary>
            /// 前回対象となったキャラクター
            /// </summary>
            OnChaseTakeDamages,
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
                        
            /// <summary>素早さ</summary>
            Speed,
            
            /// <summary>運</summary>
            Luck,
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
        /// コマンドタイプ
        /// </summary>
        public enum CommandType
        {
            /// <summary>
            /// 攻撃
            /// </summary>
            Attack,
            
            /// <summary>
            /// 回復
            /// </summary>
            Recovery,
            
            /// <summary>
            /// バフ
            /// </summary>
            Buff,
            
            /// <summary>
            /// デバフ
            /// </summary>
            Debuff,
            
            /// <summary>
            /// パラメータ上昇
            /// </summary>
            ParameterUp,
            
            /// <summary>
            /// パラメータ減少
            /// </summary>
            ParameterDown,
        }

        /// <summary>
        /// コマンド実行後の処理タイプ
        /// </summary>
        public enum PostprocessCommand
        {
            /// <summary>
            /// ターンを終了する
            /// </summary>
            EndTurn,
            
            /// <summary>
            /// ターン終了イベントを完了する
            /// </summary>
            CompleteEndTurnEvent,
        }
        
        /// <summary>
        /// 武器タイプ
        /// </summary>
        [Flags]
        public enum WeaponType
        {
            /// <summary>短剣</summary>
            Dagger = 1 << 0,
            
            /// <summary>長剣</summary>
            LongSword = 1 << 1,
            
            /// <summary>盾</summary>
            Shield = 1 << 2,
            
            /// <summary>刀</summary>
            Katana = 1 << 3,
            
            /// <summary>杖</summary>
            Staff = 1 << 4,
            
            /// <summary>ロッド</summary>
            Rod = 1 << 5,

            /// <summary>弓</summary>
            Bow = 1 << 6,

            /// <summary>ナックル</summary>
            Knuckle = 1 << 7,

            /// <summary>本</summary>
            Book = 1 << 8,

            /// <summary>大剣</summary>
            LargeSword = 1 << 9,

            /// <summary>槍</summary>
            Spear = 1 << 10,

            /// <summary>ハンマー</summary>
            Hammer = 1 << 11,
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

        public const int LevelMin = 1;
        
        public const int LevelMax = 100;
    }
}
