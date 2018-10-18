using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.User
{
    /// <summary>
    /// データのインスタンスIDを管理するクラス
    /// </summary>
    [Serializable]
    public sealed class InstanceId
    {
        [SerializeField]
        private int count = 0;
        public int Count => this.count;

        /// <summary>
        /// 新規IDを発行する
        /// </summary>
        public int Issue => ++this.count;
    }
}
