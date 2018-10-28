using System;
using GL.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.CharacterControllers
{
    /// <summary>
    /// 属性相性
    /// </summary>
    [Serializable]
    public sealed class Attribute
    {
        /// <summary>
        /// 打撃
        /// </summary>
        [SerializeField][Range(-1.0f, 3.0f)]
        private float brow;
        public float Brow => this.brow;

        /// <summary>
        /// 斬撃
        /// </summary>
        [SerializeField][Range(-1.0f, 3.0f)]
        private float slash;
        public float Slash => this.slash;

        /// <summary>
        /// 突
        /// </summary>
        [SerializeField][Range(-1.0f, 3.0f)]
        private float poke;
        public float Poke => this.poke;

        /// <summary>
        /// 無属性
        /// </summary>
        [SerializeField][Range(-1.0f, 3.0f)]
        private float no;
        public float No => this.no;

        /// <summary>
        /// 炎
        /// </summary>
        [SerializeField][Range(-1.0f, 3.0f)]
        private float fire;
        public float Fire => this.fire;

        /// <summary>
        /// 水
        /// </summary>
        [SerializeField][Range(-1.0f, 3.0f)]
        private float water;
        public float Water => this.water;

        /// <summary>
        /// 雷
        /// </summary>
        [SerializeField][Range(-1.0f, 3.0f)]
        private float thunder;
        public float Thunder => this.thunder;

        public Attribute()
        {
            this.Reset();
        }

        public Attribute(Attribute other)
        {
            this.brow = other.brow;
            this.slash = other.slash;
            this.poke = other.poke;
            this.no = other.no;
            this.fire = other.fire;
            this.water = other.water;
            this.thunder = other.thunder;
        }

        public Attribute(CharacterRecord characterRecord)
            : this(characterRecord.Attribute)
        {
        }

        public float Get(Constants.AttributeType type)
        {
            switch(type)
            {
                case Constants.AttributeType.Brow:
                    return this.brow;
                case Constants.AttributeType.Slash:
                    return this.slash;
                case Constants.AttributeType.Poke:
                    return this.poke;
                case Constants.AttributeType.No:
                    return this.No;
                case Constants.AttributeType.Fire:
                    return this.fire;
                case Constants.AttributeType.Water:
                    return this.water;
                case Constants.AttributeType.Thunder:
                    return this.thunder;
                default:
                    Assert.IsTrue(false, $"{type}は未対応の値です");
                    return 0;
            }
        }

        public void Reset()
        {
            this.brow = 0.0f;
            this.slash = 0.0f;
            this.poke = 0.0f;
            this.no = 0.0f;
            this.fire = 0.0f;
            this.water = 0.0f;
            this.thunder = 0.0f;
        }
    }
}
