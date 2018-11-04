using System;
using GL.Database;
using HK.Framework.Text;
using HK.GL.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.User
{
    /// <summary>
    /// <see cref="UserData"/>が持つ装備品クラス
    /// </summary>
    [Serializable]
    public sealed class Equipment : IInstanceId
    {
        [SerializeField][HideInInspector]
        private int instanceId;
        public int InstanceId => this.instanceId;

        [SerializeField]
        private string id;

        /// <summary>
        /// <see cref="Battle.WeaponRecord"/>に紐づくID
        /// </summary>
        public string Id => this.id;

        public Equipment(InstanceId instanceId, string id)
        {
            this.instanceId = instanceId.Issue;
            this.id = id;
        }

        public Equipment Clone(InstanceId instanceId)
        {
            return new Equipment(instanceId, this.id);
        }

        public EquipmentRecord EquipmentRecord => MasterData.Equipment.GetById(this.Id);
    }
}
