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
    public sealed class Party
    {
        /// <summary>
        /// パーティの最大人数
        /// </summary>
        public const int PlayerMax = 4;
        
        /// <summary>
        /// このパーティーに所属しているプレイヤーのインデックス
        /// </summary>
        [SerializeField]
        public List<int> PlayerIds = new List<int>();

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
                    PlayerIds = new List<int>(this.PlayerIds)
                };
            }
        }

        public GL.Battle.PartyControllers.Blueprint AsBlueprint
        {
            get
            {
                return GL.Battle.PartyControllers.Blueprint.CloneAsPlayerParty(this);
            }
        }
    }
}
