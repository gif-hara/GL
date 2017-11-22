using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

namespace HK.GL.Battle
{
    /// <summary>
    /// キャラクターのステータス.
    /// </summary>
    public sealed class CharacterStatus
    {
        /// <summary>
        /// キャラクター名
        /// </summary>
        public string Name { private set; get; }

        /// <summary>
        /// ヒットポイント最大値
        /// </summary>
        public int HitPointMax { private set; get; }

        /// <summary>
        /// ヒットポイント
        /// </summary>
        public int HitPoint { private set; get; }

        /// <summary>
        /// 攻撃力
        /// </summary>
        public int Strength { private set; get; }

        /// <summary>
        /// 防御力
        /// </summary>
        public int Defense { private set; get; }

        /// <summary>
        /// 思いやり力
        /// </summary>
        /// <remarks>
        /// バフ系の上昇量に影響する
        /// </remarks>
        public int Sympathy { private set; get; }

        /// <summary>
        /// ネガキャン力
        /// </summary>
        /// <remarks>
        /// デバフ系の上昇量に影響する
        /// </remarks>
        public int Nega { private set; get; }

        /// <summary>
        /// 素早さ
        /// </summary>
        public int Speed { private set; get; }

        /// <summary>
        /// 使用可能なコマンド
        /// </summary>
        public List<ICommand> Commands { private set; get; }

        /// <summary>
        /// 待機した量
        /// </summary>
        public float Wait { set; get; }

        /// <summary>
        /// 元となるスペック
        /// </summary>
        public CharacterStatusSettings BaseSpec{ private set; get; }

        /// <summary>
        /// 死亡しているか返す
        /// </summary>
        public bool IsDead
        {
            get
            {
                return this.HitPoint <= 0;
            }
        }

        public CharacterStatus(CharacterStatusSettings baseSpec)
        {
            this.BaseSpec = baseSpec;
            this.Name = this.BaseSpec.Name;
            this.HitPointMax = this.BaseSpec.HitPoint;
            this.HitPoint = this.HitPointMax;
            this.Strength = this.BaseSpec.Strength;
            this.Defense = this.BaseSpec.Defense;
            this.Sympathy = this.BaseSpec.Sympathy;
            this.Nega = this.BaseSpec.Nega;
            this.Speed = this.BaseSpec.Speed;
            this.Commands = this.BaseSpec.Commands;
            this.Wait = 0.0f;
        }

        public void AddDefense(int value)
        {
            this.Defense += value;
        }

        public void TakeDamage(int damage)
        {
            this.HitPoint -= damage;
        }
    }
}
