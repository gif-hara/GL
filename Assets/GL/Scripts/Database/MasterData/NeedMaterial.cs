using System;
using HK.Framework.Text;
using HK.GL.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Database
{
    /// <summary>
    /// 作成に必要な素材を持つクラス
    /// </summary>
    [Serializable]
    public sealed class NeedMaterial
    {
        [SerializeField]
        private MaterialRecord material;
        public MaterialRecord Material => this.material;

        [SerializeField]
        private int amount;
        public int Amount => this.amount;

#if UNITY_EDITOR
        public NeedMaterial(MaterialRecord material, int amount)
        {
            this.material = material;
            this.amount = amount;
        }
#endif
    }
}
