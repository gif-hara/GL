using System;
using GL.Database;
using HK.Framework.Text;
using HK.GL.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.User
{
    /// <summary>
    /// <see cref="UserData"/>が持つ武器クラス
    /// </summary>
    [Serializable]
    public sealed class Weapon : IInstanceId
    {
        [SerializeField][HideInInspector]
        private int instanceId;
        public int InstanceId => this.instanceId;

        [SerializeField]
        private string id;

        /// <summary>
        /// <see cref="Battle.Weapon"/>に紐づくID
        /// </summary>
        public string Id => this.id;

        public Weapon(InstanceId instanceId, string id)
        {
            this.instanceId = instanceId.Issue;
            this.id = id;
        }

        public Weapon Clone(InstanceId instanceId)
        {
            return new Weapon(instanceId, this.id);
        }

        public Battle.Weapon BattleWeapon => MasterData.Weapon.GetById(this.Id);
    }
}
