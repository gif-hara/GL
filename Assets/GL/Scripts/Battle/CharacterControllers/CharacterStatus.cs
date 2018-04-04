using System;
using UnityEngine;

namespace GL.Scripts.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターのステータス
    /// </summary>
    [Serializable]
    public sealed class CharacterStatus
    {
        /// <summary>
        /// キャラクター名
        /// </summary>
        [SerializeField]
        public string Name;

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

        public CharacterStatus(Blueprint blueprint)
        {
            this.Name = blueprint.Status.Name;
            this.HitPoint = blueprint.Status.HitPoint;
            this.Strength = blueprint.Status.Strength;
            this.Defense = blueprint.Status.Defense;
            this.Sympathy = blueprint.Status.Sympathy;
            this.Nega = blueprint.Status.Nega;
            this.Speed = blueprint.Status.Speed;
        }
    }
}
