using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GL.Scripts.User
{
    /// <summary>
    /// ユーザーデータのパーティクラス
    /// </summary>
    [Serializable]
    public sealed class Party
    {
        /// <summary>
        /// パーティの最大人数
        /// </summary>
        public const int PlayerMax = 4;
        
        [SerializeField]
        public List<Player> Players = new List<Player>();

        /// <summary>
        /// 自分自身のクローンを返す
        /// </summary>
        /// <remarks>
        /// <see cref="Player"/>をクローンしないと<see cref="Player.Id"/>が発行されないので注意
        /// </remarks>
        public Party Clone
        {
            get
            {
                return new Party()
                {
                    Players = this.Players.Select(x => x.Clone).ToList()
                };
            }
        }

        public Battle.PartyControllers.Blueprint AsBlueprint
        {
            get
            {
                return Battle.PartyControllers.Blueprint.CloneAsPlayerParty(this);
            }
        }
    }
}
