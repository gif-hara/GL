using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

namespace HK.GL.Battle
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
