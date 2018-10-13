using System;
using System.Collections.Generic;
using GL.Battle;
using GL.Battle.Accessories;
using GL.Battle.CharacterControllers;
using UnityEngine;

namespace GL.User
{
    /// <summary>
    /// ユーザーデータのプレイヤークラス
    /// </summary>
    [Serializable]
    public sealed class Player
    {
        [SerializeField][HideInInspector]
        public string Id = Guid.NewGuid().ToString();

        [SerializeField]
        public string PlayerName;
        
        [SerializeField][Range(1.0f, 100.0f)]
        public int Level = 1;
        
        [SerializeField]
        public Blueprint Blueprint;

        [SerializeField]
        public int WeaponId;

        [SerializeField]
        public List<Accessory> Accessories;

        public Parameter Parameter { get { return this.Blueprint.GetParameter(this.Level); } }

        public Resistance Resistance { get { return this.Blueprint.Resistance; } }

        private Player()
        {
        }

        /// <summary>
        /// 自分自身のクローンを返す
        /// </summary>
        /// <remarks>
        /// 動的に生成しないと<see cref="Id"/>が発行されないので基本的にクローンする
        /// </remarks>
        public Player Clone
        {
            get
            {
                return Create(this.PlayerName, this.Level, this.Blueprint);
            }
        }

        private static Player Create(string playerName, int level, Blueprint blueprint)
        {
            return new Player()
            {
                Id = Guid.NewGuid().ToString(),
                PlayerName = playerName,
                Level = level,
                Blueprint = blueprint
            };
        }

        public User.Weapon Weapon
        {
            get
            {
                return UserData.Load().Weapons[this.WeaponId];
            }
        }
    }
}
