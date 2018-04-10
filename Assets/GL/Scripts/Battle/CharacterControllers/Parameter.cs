using System;
using GL.Scripts.Battle.Systems;
using UnityEngine;
using UnityEngine.Assertions;
using Blueprint = GL.Scripts.Battle.PartyControllers.Blueprints.Blueprint;

namespace GL.Scripts.Battle.CharacterControllers
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
        [SerializeField][Range(0.0f, 255.0f)]
        public int Strength;

        /// <summary>
        /// 防御力
        /// </summary>
        [SerializeField][Range(0.0f, 255.0f)]
        public int Defense;

        /// <summary>
        /// 思いやり力
        /// </summary>
        /// <remarks>
        /// バフ系の上昇量に影響する
        /// </remarks>
        [SerializeField][Range(0.0f, 255.0f)]
        public int Sympathy;

        /// <summary>
        /// ネガキャン力
        /// </summary>
        /// <remarks>
        /// デバフ系の上昇量に影響する
        /// </remarks>
        [SerializeField][Range(0.0f, 255.0f)]
        public int Nega;

        /// <summary>
        /// 素早さ
        /// </summary>
        [SerializeField][Range(0.0f, 255.0f)]
        public int Speed;

        /// <summary>
        /// 運
        /// </summary>
        [SerializeField][Range(0.0f, 255.0f)]
        public int Luck;

        public Parameter()
        {
            this.Reset();
        }

        public Parameter(Parameter other)
        {
            this.HitPoint = other.HitPoint;
            this.Strength = other.Strength;
            this.Defense = other.Defense;
            this.Sympathy = other.Sympathy;
            this.Nega = other.Nega;
            this.Speed = other.Speed;
            this.Luck = other.Luck;
        }

        public Parameter(Blueprints.Blueprint blueprint, int level)
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
                case Constants.StatusParameterType.Defense:
                    this.Defense += value;
                    break;
                case Constants.StatusParameterType.Sympathy:
                    this.Sympathy += value;
                    break;
                case Constants.StatusParameterType.Nega:
                    this.Nega += value;
                    break;
                case Constants.StatusParameterType.Speed:
                    this.Speed += value;
                    break;
                case Constants.StatusParameterType.Luck:
                    this.Luck += value;
                    break;
                default:
                    Assert.IsTrue(false, string.Format("未対応の値です {0}", type));
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
                case Constants.StatusParameterType.Defense:
                    return this.Defense;
                case Constants.StatusParameterType.Sympathy:
                    return this.Sympathy;
                case Constants.StatusParameterType.Nega:
                    return this.Nega;
                case Constants.StatusParameterType.Speed:
                    return this.Speed;
                case Constants.StatusParameterType.Luck:
                    return this.Luck;
                default:
                    Assert.IsTrue(false, string.Format("未対応の値です {0}", type));
                    return 0;
            }
        }

        public void Reset()
        {
            this.HitPoint = 0;
            this.Strength = 0;
            this.Defense = 0;
            this.Sympathy = 0;
            this.Nega = 0;
            this.Speed = 0;
            this.Luck = 0;
        }
    }
}
