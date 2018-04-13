using System;
using System.Collections.Generic;
using GL.Scripts.Battle.Accessories;
using GL.Scripts.Battle.Weapons;
using UnityEngine;

namespace GL.Scripts.User
{
    /// <summary>
    /// ユーザーデータのプレイヤークラス
    /// </summary>
    [Serializable]
    public sealed class Player
    {
        [SerializeField][HideInInspector]
        public string Id = Guid.NewGuid().ToString();
        
        [SerializeField][Range(1.0f, 100.0f)]
        public int Level;
        
        [SerializeField]
        public Battle.CharacterControllers.Blueprints.Player Blueprint;

        [SerializeField]
        public Weapon Weapon;

        [SerializeField]
        public List<Accessory> Accessories;

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
                return Create(this.Level, this.Blueprint);
            }
        }

        public static Player Create(int level, Battle.CharacterControllers.Blueprints.Player blueprint)
        {
            return new Player(){Id = Guid.NewGuid().ToString(), Level = level, Blueprint = blueprint};
        }
    }
}
