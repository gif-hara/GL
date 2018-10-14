using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.User
{
    /// <summary>
    /// <see cref="UserData"/>が持つアクセサリークラス
    /// </summary>
    [Serializable]
    public sealed class Accessory
    {
        [SerializeField]
        private string id;

        /// <summary>
        /// <see cref="Battle.Accessory"/>に紐づくID
        /// </summary>
        public string Id => this.id;
    }
}
