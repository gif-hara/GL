using System.Collections.Generic;
using GL.Scripts.Battle.Systems;
using UnityEngine.Assertions;

namespace GL.Scripts.Battle.CharacterControllers.Party
{
    /// <summary>
    /// バトルに参加しているパーティ
    /// </summary>
    public sealed class Parties
    {
        public CharacterControllers.Party.Party Player { private set; get; }

        public CharacterControllers.Party.Party Enemy { private set; get; }

        /// <summary>
        /// 今回のバトルに参加しているキャラクター全てのリスト
        /// </summary>
        public List<Character> AllMember { private set; get; }

        public Parties(CharacterControllers.Party.Party player, CharacterControllers.Party.Party enemy)
        {
            this.Player = player;
            this.Enemy = enemy;
            this.AllMember = new List<Character>();
            this.AllMember.AddRange(this.Player.Members);
            this.AllMember.AddRange(this.Enemy.Members);
        }

        /// <summary>
        /// 味方パーティを返す
        /// </summary>
        public CharacterControllers.Party.Party Ally(Character character)
        {
            return character.CharacterType == Constants.CharacterType.Player
                ? this.Player
                : this.Enemy;
        }

        /// <summary>
        /// 敵対しているパーティを返す
        /// </summary>
        public CharacterControllers.Party.Party Opponent(Character character)
        {
            return character.CharacterType == Constants.CharacterType.Player
                ? this.Enemy
                : this.Player;
        }

        /// <summary>
        /// <paramref name="targetPartyType"/>からパーティを返す
        /// </summary>
        public CharacterControllers.Party.Party GetFromTargetPartyType(Character character, Constants.TargetPartyType targetPartyType)
        {
            switch (targetPartyType)
            {
                case Constants.TargetPartyType.Ally:
                    return this.Ally(character);
                case Constants.TargetPartyType.Opponent:
                    return this.Opponent(character);
                default:
                    Assert.IsTrue(false, string.Format("未対応の値です {0}", targetPartyType));
                    return null;
            }
        }

        /// <summary>
        /// 勝敗結果を返す
        /// </summary>
        /// <remarks>
        /// 両者とも全て死亡した場合は敵の勝利とする
        /// </remarks>
        public Constants.BattleResult Result
        {
            get
            {
                var isPlayerAlive = this.Player.Members.FindIndex(c => !c.StatusController.IsDead) != -1;
                var isEnemyAlive = this.Enemy.Members.FindIndex(c => !c.StatusController.IsDead) != -1;
                if(isPlayerAlive && isEnemyAlive)
                {
                    return Constants.BattleResult.Unsettlement;
                }
                if(isEnemyAlive)
                {
                    return Constants.BattleResult.EnemyWin;
                }

                return Constants.BattleResult.PlayerWin;
            }
        }
    }
}
