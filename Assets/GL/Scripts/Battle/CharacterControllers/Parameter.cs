using System;
using GL.Scripts.Battle.Systems;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Scripts.Battle.CharacterControllers
{
    [Serializable]
    public class Parameter
    {
        /// <summary>
        /// ヒットポイント
        /// </summary>
        [SerializeField]
        public int HitPoint;

        /// <summary>
        /// 攻撃力
        /// </summary>
        [SerializeField]
        public int Strength;

        /// <summary>
        /// 防御力
        /// </summary>
        [SerializeField]
        public int Defense;

        /// <summary>
        /// 思いやり力
        /// </summary>
        /// <remarks>
        /// バフ系の上昇量に影響する
        /// </remarks>
        [SerializeField]
        public int Sympathy;

        /// <summary>
        /// ネガキャン力
        /// </summary>
        /// <remarks>
        /// デバフ系の上昇量に影響する
        /// </remarks>
        [SerializeField]
        public int Nega;

        /// <summary>
        /// 素早さ
        /// </summary>
        [SerializeField]
        public int Speed;

        public Parameter()
        {
            this.HitPoint = 0;
            this.Strength = 0;
            this.Defense = 0;
            this.Sympathy = 0;
            this.Nega = 0;
            this.Speed = 0;
        }

        public Parameter(Blueprint blueprint)
        {
            this.HitPoint = blueprint.Status.Parameter.HitPoint;
            this.Strength = blueprint.Status.Parameter.Strength;
            this.Defense = blueprint.Status.Parameter.Defense;
            this.Sympathy = blueprint.Status.Parameter.Sympathy;
            this.Nega = blueprint.Status.Parameter.Nega;
            this.Speed = blueprint.Status.Parameter.Speed;
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
                default:
                    Assert.IsTrue(false, string.Format("未対応の値です {0}", type));
                    return 0;
            }
        }
    }
}
