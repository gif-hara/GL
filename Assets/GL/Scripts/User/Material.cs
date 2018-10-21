using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.User
{
    /// <summary>
    /// ユーザーデータ用の素材クラス
    /// </summary>
    [Serializable]
    public sealed class Material
    {
        [SerializeField]
        private string id;
        public string Id => this.id;

        [SerializeField]
        private int count;
        public int Count { get { return this.count; } set { this.count = value; } }

        public bool IsEnough(int value) => this.count >= value;

        public Material(string id)
        {
            this.id = id;
        }
    }
}
