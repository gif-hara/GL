using System;
using System.Collections.Generic;
using UnityEngine;

namespace GL.Scripts.User
{
    /// <summary>
    /// ユーザーデータのパーティクラス
    /// </summary>
    [Serializable]
    public sealed class Party
    {
        [SerializeField]
        public List<Player> Players = new List<Player>();
    }
}
