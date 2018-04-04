namespace GL.Scripts.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターのステータス
    /// </summary>
    public sealed class CharacterStatus
    {
        /// <summary>
        /// キャラクター名
        /// </summary>
        public string Name { set; get; }

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

        public CharacterStatus(CharacterStatusSettings settings)
        {
            this.Name = settings.Name;
            this.HitPoint = settings.HitPoint;
            this.Strength = settings.Strength;
            this.Defense = settings.Defense;
            this.Sympathy = settings.Sympathy;
            this.Nega = settings.Nega;
            this.Speed = settings.Speed;
        }
    }
}
