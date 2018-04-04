using System.Collections.Generic;
using GL.Scripts.Battle.Commands.Impletents;

namespace GL.Scripts.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターのステータス.
    /// </summary>
    public sealed class CharacterStatus
    {
        /// <summary>
        /// キャラクター名
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// ヒットポイント最大値
        /// </summary>
        public int HitPointMax { get { return this.BaseSpec.HitPoint; } }

        /// <summary>
        /// ヒットポイント
        /// </summary>
        public int HitPoint { set; get; }

        /// <summary>
        /// 攻撃力
        /// </summary>
        public int Strength { set; get; }

        /// <summary>
        /// 防御力
        /// </summary>
        public int Defense { set; get; }

        /// <summary>
        /// 思いやり力
        /// </summary>
        /// <remarks>
        /// バフ系の上昇量に影響する
        /// </remarks>
        public int Sympathy { set; get; }

        /// <summary>
        /// ネガキャン力
        /// </summary>
        /// <remarks>
        /// デバフ系の上昇量に影響する
        /// </remarks>
        public int Nega { set; get; }

        /// <summary>
        /// 素早さ
        /// </summary>
        public int Speed { set; get; }

        /// <summary>
        /// 使用可能なコマンド
        /// </summary>
        public List<IImplement> Commands { private set; get; }

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
            this.HitPoint = this.BaseSpec.HitPoint;
            this.Strength = this.BaseSpec.Strength;
            this.Defense = this.BaseSpec.Defense;
            this.Sympathy = this.BaseSpec.Sympathy;
            this.Nega = this.BaseSpec.Nega;
            this.Speed = this.BaseSpec.Speed;
            this.Commands = this.BaseSpec.Commands;
            this.Wait = 0.0f;
        }
    }
}
