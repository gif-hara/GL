using System.Collections.Generic;
using System.Linq;
using GL.Scripts.Battle.Commands.Impletents;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターのステータスを制御するクラス.
    /// </summary>
    public sealed class CharacterStatusController
    {
        /// <summary>
        /// 基本のステータス
        /// </summary>
        public CharacterStatus BaseStatus { private set; get; }

        /// <summary>
        /// 加算されるステータス
        /// </summary>
        /// <remarks>
        /// バフデバフによる影響値が入ります
        /// </remarks>
        public CharacterStatus AdditiveStatus { private set; get; }

        /// <summary>
        /// 使用可能なコマンド
        /// </summary>
        public IImplement[] Commands { private set; get; }

        /// <summary>
        /// 待機した量
        /// </summary>
        public float Wait { set; get; }

        /// <summary>
        /// 元となる設計図
        /// </summary>
        public Blueprint Blueprint{ private set; get; }

        public string Name { get { return this.BaseStatus.Name; } }

        /// <summary>
        /// ヒットポイント最大値
        /// </summary>
        public int HitPointMax { get { return this.Blueprint.Status.HitPoint; } }

        public int HitPoint { set { this.BaseStatus.HitPoint = value; } get { return this.BaseStatus.HitPoint; } }

        /// <summary>
        /// 死亡しているか返す
        /// </summary>
        public bool IsDead { get { return this.HitPoint <= 0; } }

        public int TotalStrength { get { return this.BaseStatus.Strength + this.AdditiveStatus.Strength; } }

        public int TotalDefense { get { return this.BaseStatus.Defense + this.AdditiveStatus.Defense; } }
        
        public int TotalSympathy { get { return this.BaseStatus.Sympathy + this.AdditiveStatus.Sympathy; } }
        
        public int TotalNega { get { return this.BaseStatus.Nega + this.AdditiveStatus.Nega; } }
        
        public int TotalSpeed { get { return this.BaseStatus.Speed + this.AdditiveStatus.Speed; } }

        public CharacterStatusController(Blueprint blueprint)
        {
            this.Blueprint = blueprint;
            this.BaseStatus = new CharacterStatus(this.Blueprint);
            this.AdditiveStatus = new CharacterStatus();
            this.Commands = this.Blueprint.Commands.Select(x => x.Create()).ToArray();
            this.Wait = 0.0f;
        }

        public void AddStatusParameter(Constants.StatusParameterType type, int value)
        {
            switch (type)
            {
                case Constants.StatusParameterType.Strength:
                    this.AdditiveStatus.Strength += value;
                    break;
                case Constants.StatusParameterType.Defense:
                    this.AdditiveStatus.Defense += value;
                    break;
                case Constants.StatusParameterType.Sympathy:
                    this.AdditiveStatus.Sympathy += value;
                    break;
                case Constants.StatusParameterType.Nega:
                    this.AdditiveStatus.Nega += value;
                    break;
                case Constants.StatusParameterType.Speed:
                    this.AdditiveStatus.Speed += value;
                    break;
            }
        }
    }
}
