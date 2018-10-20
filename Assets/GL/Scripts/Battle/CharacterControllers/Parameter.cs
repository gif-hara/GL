using System;
using GL.Battle;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.CharacterControllers
{
    [Serializable]
    public class Parameter
    {
        /// <summary>
        /// ヒットポイント
        /// </summary>
        [SerializeField][Range(0.0f, 9999.0f)]
        public int HitPoint;

        /// <summary>
        /// 攻撃力
        /// </summary>
        [SerializeField][Range(0.0f, 1000.0f)]
        public int Strength;

        /// <summary>
        /// 魔法攻撃力
        /// </summary>
        [SerializeField][Range(0.0f, 1000.0f)]
        public int StrengthMagic;

        /// <summary>
        /// 防御力
        /// </summary>
        [SerializeField][Range(0.0f, 1000.0f)]
        public int Defense;

        /// <summary>
        /// 魔法防御力
        /// </summary>
        [SerializeField][Range(0.0f, 1000.0f)]
        public int DefenseMagic;

        /// <summary>
        /// 素早さ
        /// </summary>
        [SerializeField][Range(0.0f, 1000.0f)]
        public int Speed;

        public Parameter()
        {
            this.Reset();
        }

        public Parameter(Parameter other)
        {
            this.HitPoint = other.HitPoint;
            this.Strength = other.Strength;
            this.Defense = other.Defense;
            this.Speed = other.Speed;
        }

        public Parameter(Blueprint blueprint, int level)
            : this(blueprint.GetParameter(level))
        {
        }

        public void Add(Constants.StatusParameterType type, int value)
        {
            switch (type)
            {
                case Constants.StatusParameterType.HitPoint:
                    this.HitPoint += value;
                    break;
                case Constants.StatusParameterType.Strength:
                    this.Strength += value;
                    break;
                case Constants.StatusParameterType.StrengthMagic:
                    this.StrengthMagic += value;
                    break;
                case Constants.StatusParameterType.Defense:
                    this.Defense += value;
                    break;
                case Constants.StatusParameterType.DefenseMagic:
                    this.DefenseMagic += value;
                    break;
                case Constants.StatusParameterType.Speed:
                    this.Speed += value;
                    break;
                default:
                    Assert.IsTrue(false, $"{type}は未対応の値です");
                    break;
            }
        }

        public int Get(Constants.StatusParameterType type)
        {
            switch (type)
            {
                case Constants.StatusParameterType.HitPoint:
                    return this.HitPoint;
                case Constants.StatusParameterType.Strength:
                    return this.Strength;
                case Constants.StatusParameterType.StrengthMagic:
                    return this.StrengthMagic;
                case Constants.StatusParameterType.Defense:
                    return this.Defense;
                case Constants.StatusParameterType.DefenseMagic:
                    return this.DefenseMagic;
                case Constants.StatusParameterType.Speed:
                    return this.Speed;
                default:
                    Assert.IsTrue(false, $"{type}は未対応の値です");
                    return 0;
            }
        }

        public void Reset()
        {
            this.HitPoint = 0;
            this.Strength = 0;
            this.StrengthMagic = 0;
            this.Defense = 0;
            this.DefenseMagic = 0;
            this.Speed = 0;
        }
    }
}
