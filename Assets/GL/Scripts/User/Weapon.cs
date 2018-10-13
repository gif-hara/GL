using System;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.User
{
    /// <summary>
    /// <see cref="UserData"/>が持つ武器クラス
    /// </summary>
    [Serializable]
    public sealed class Weapon
    {
        [SerializeField]
        private string id;
        
        /// <summary>
        /// <see cref="Battle.Weapon"/>に紐づくID
        /// </summary>
        public string Id { get { return this.id; } }
    }
}
