using System;
using System.Collections.Generic;
using System.Linq;
using GL.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.User
{
    /// <summary>
    /// ユーザーデータのパーティクラス
    /// </summary>
    [Serializable]
    public sealed class Party : IInstanceId
    {
        /// <summary>
        /// パーティの最大人数
        /// </summary>
        public const int PlayerMax = 4;

        [SerializeField][HideInInspector]
        private int instanceId;
        public int InstanceId => this.instanceId;

        /// <summary>
        /// このパーティーに所属しているプレイヤーのインスタンスIDリスト
        /// </summary>
        [SerializeField]
        public List<int> PlayerInstanceIds = new List<int>();

        /// <summary>
        /// 自分自身のクローンを返す
        /// </summary>
        public Party Clone(InstanceId instanceId)
        {
            return new Party()
            {
                instanceId = instanceId.Issue,
                PlayerInstanceIds = new List<int>(this.PlayerInstanceIds)
            };
        }

        /// <summary>
        /// プレイヤーを交代する
        /// </summary>
        public void Change(int targetInstanceId, int changeInstanceId)
        {
            var targetInstanceIdIndex = this.PlayerInstanceIds.FindIndex(p => p == targetInstanceId);
            var changeInstanceIdIndex = this.PlayerInstanceIds.FindIndex(p => p == changeInstanceId);

            // 交代するプレイヤーがパーティに存在する場合は順番を入れ替える
            if(targetInstanceIdIndex >= 0 && changeInstanceIdIndex >= 0)
            {
                this.PlayerInstanceIds[targetInstanceIdIndex] = changeInstanceId;
                this.PlayerInstanceIds[changeInstanceIdIndex] = targetInstanceId;
            }
            else if(changeInstanceIdIndex >= 0)
            {
                this.PlayerInstanceIds[changeInstanceIdIndex] = targetInstanceId;
            }
            else
            {
                this.PlayerInstanceIds[targetInstanceIdIndex] = changeInstanceId;
            }
        }

        /// <summary>
        /// <paramref name="player"/>がパーティに参加しているか返す
        /// </summary>
        public bool Contains(Player player)
        {
            return this.PlayerInstanceIds.FindIndex(i => i == player.InstanceId) >= 0;
        }

        /// <summary>
        /// バトル用にデータを変換する
        /// </summary>
        /// <returns></returns>
        public PartyRecord AsPartyRecord => PartyRecord.CloneAsPlayerParty(this);

        /// <summary>
        /// プレイヤーリストを返す
        /// </summary>
        public Player[] AsPlayers => this.PlayerInstanceIds.Select(instanceId => UserData.Instance.Players.GetByInstanceId(instanceId)).ToArray();
    }
}
