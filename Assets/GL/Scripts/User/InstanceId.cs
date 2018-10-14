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
        public int Count { get; private set; } = 0;

        /// <summary>
        /// 新規IDを発行する
        /// </summary>
        public int Issue => ++this.Count;
    }
}
