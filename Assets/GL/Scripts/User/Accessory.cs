using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.User
{
    /// <summary>
    /// <see cref="UserData"/>が持つアクセサリークラス
    /// </summary>
    [Serializable]
    public sealed class Accessory : IInstanceId
    {
        [SerializeField][HideInInspector]
        private int instanceId;
        public int InstanceId => this.instanceId;

        [SerializeField]
        private string id;

        /// <summary>
        /// <see cref="Battle.AccessoryRecord"/>に紐づくID
        /// </summary>
        public string Id => this.id;

        public Accessory Clone(InstanceId instanceId)
        {
            return new Accessory
            {
                instanceId = instanceId.Issue,
                id = this.id
            };
        }
    }
}
