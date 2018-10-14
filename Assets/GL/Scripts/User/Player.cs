using System;
using System.Collections.Generic;
using GL.Battle.CharacterControllers;
using GL.MasterData;
using HK.GL.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

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
        public string BlueprintId;

        [SerializeField]
        public int WeaponId;

        [SerializeField]
        public List<Battle.Accessory> Accessories = new List<Battle.Accessory>();

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
                return Create(this.PlayerName, this.Level, this.BlueprintId, this.WeaponId);
            }
        }

        private static Player Create(string playerName, int level, string blueprintId, int weaponId)
        {
            return new Player()
            {
                Id = Guid.NewGuid().ToString(),
                PlayerName = playerName,
                Level = level,
                BlueprintId = blueprintId,
                WeaponId = weaponId
            };
        }

        public User.Weapon Weapon
        {
            get
            {
                return UserData.Load().Weapons[this.WeaponId];
            }
        }

        public Battle.CharacterControllers.Blueprint Blueprint
        {
            get
            {
                var result = Database.Character.List.Find(b => b.Id == this.BlueprintId);
                Assert.IsNotNull(result, string.Format("Id = {0}の{1}が存在しません", this.BlueprintId, typeof(Battle.CharacterControllers.Blueprint).Name));

                return result;
            }
        }
    }
}
