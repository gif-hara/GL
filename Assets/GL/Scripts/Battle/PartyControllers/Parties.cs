using System.Collections.Generic;
using GL.Battle.CharacterControllers;
using GL.Battle;
using UnityEngine.Assertions;
using HK.Framework.EventSystems;
using GL.Events.Battle;

namespace GL.Battle.PartyControllers
{
    /// <summary>
    /// バトルに参加しているパーティ
    /// </summary>
    public sealed class Parties
    {
        public Party Player { private set; get; }

        public Party Enemy { private set; get; }

        /// <summary>
        /// 今回のバトルに参加しているキャラクター全てのリスト
        /// </summary>
        public List<Character> AllMember { private set; get; }

        public Parties(Party player, Party enemy)
        {
            this.Player = player;
            this.Enemy = enemy;
            this.AllMember = new List<Character>();
            this.AllMember.AddRange(this.Player.Members);
            this.AllMember.AddRange(this.Enemy.Members);
            Broker.Global.Publish(CreatedParties.Get(this));
        }

        /// <summary>
        /// 味方パーティを返す
        /// </summary>
        public Party Ally(Character character)
        {
            return character.CharacterType == Constants.CharacterType.Player
                ? this.Player
                : this.Enemy;
        }

        /// <summary>
        /// 敵対しているパーティを返す
        /// </summary>
        public Party Opponent(Character character)
        {
            return character.CharacterType == Constants.CharacterType.Player
                ? this.Enemy
                : this.Player;
        }

        /// <summary>
        /// <paramref name="targetPartyType"/>からパーティを返す
        /// </summary>
        public Party GetFromTargetPartyType(Character character, Constants.TargetPartyType targetPartyType)
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

        public Character[] GetTargets(Character target, Constants.TargetType type)
        {
            switch(type)
            {
                case Constants.TargetType.Select:
                    return new Character[] { target };
                case Constants.TargetType.SelectRange:
                    var party = this.Ally(target);
                    var result = new List<Character>();
                    result.Add(target);
                    var members = party.Members;
                    var targetIndex = members.FindIndex(c => c == target);
                    // 右側の敵を取得
                    for (var i = targetIndex + 1; i < members.Count; i++)
                    {
                        var m = members[i];
                        if (!m.StatusController.IsDead)
                        {
                            result.Add(m);
                            break;
                        }
                    }
                    // 左側の敵を取得
                    for (var i = targetIndex - 1; i >= 0; i--)
                    {
                        var m = members[i];
                        if (!m.StatusController.IsDead)
                        {
                            result.Add(m);
                            break;
                        }
                    }
                    return result.ToArray();
                default:
                    Assert.IsTrue(false, $"{type}は未対応の値です");
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
