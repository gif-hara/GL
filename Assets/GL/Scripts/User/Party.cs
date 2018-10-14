using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        /// バトル用にデータを変換する
        /// </summary>
        /// <returns></returns>
        public GL.Battle.PartyControllers.Blueprint AsBlueprint => GL.Battle.PartyControllers.Blueprint.CloneAsPlayerParty(this);

        /// <summary>
        /// プレイヤーリストを返す
        /// </summary>
        public Player[] AsPlayers => this.PlayerInstanceIds.Select(instanceId => UserData.Instance.Players.GetByInstanceId(instanceId)).ToArray();
    }
}
