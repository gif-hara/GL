using System.Collections.Generic;
using System.Linq;
using GL.Scripts.Battle.Commands.Implements;
using GL.Scripts.Battle.Systems;
using UnityEngine.Assertions;

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
        public CharacterStatus Base { private set; get; }

        /// <summary>
        /// バトル中に加算されるステータス
        /// </summary>
        /// <remarks>
        /// バフデバフによる影響値が入ります
        /// </remarks>
        public CharacterStatus Dynamic { private set; get; }
        
        /// <summary>
        /// アクセサリーによって加算されるステータス
        /// </summary>
        public CharacterStatus Accessory { private set; get; }

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

        public string Name { get { return this.Base.Name; } }

        /// <summary>
        /// ヒットポイント最大値
        /// </summary>
        public int HitPointMax { get { return this.Blueprint.Status.HitPoint; } }

        public int HitPoint { set { this.Base.HitPoint = value; } get { return this.Base.HitPoint; } }

        /// <summary>
        /// 死亡しているか返す
        /// </summary>
        public bool IsDead { get { return this.HitPoint <= 0; } }

        public int TotalStrength { get { return this.Base.Strength + this.Dynamic.Strength; } }

        public int TotalDefense { get { return this.Base.Defense + this.Dynamic.Defense; } }
        
        public int TotalSympathy { get { return this.Base.Sympathy + this.Dynamic.Sympathy; } }
        
        public int TotalNega { get { return this.Base.Nega + this.Dynamic.Nega; } }
        
        public int TotalSpeed { get { return this.Base.Speed + this.Dynamic.Speed; } }

        public CharacterStatusController(Blueprint blueprint)
        {
            this.Blueprint = blueprint;
            this.Base = new CharacterStatus(this.Blueprint);
            this.Dynamic = new CharacterStatus();
            this.Accessory = new CharacterStatus();
            this.Commands = this.Blueprint.Commands.Select(x => x.Create()).ToArray();
            this.Wait = 0.0f;
        }

        public void AddStatusParameter(Constants.StatusParameterType type, int value)
        {
            switch (type)
            {
                case Constants.StatusParameterType.Strength:
                    this.Dynamic.Strength += value;
                    break;
                case Constants.StatusParameterType.Defense:
                    this.Dynamic.Defense += value;
                    break;
                case Constants.StatusParameterType.Sympathy:
                    this.Dynamic.Sympathy += value;
                    break;
                case Constants.StatusParameterType.Nega:
                    this.Dynamic.Nega += value;
                    break;
                case Constants.StatusParameterType.Speed:
                    this.Dynamic.Speed += value;
                    break;
                default:
                    Assert.IsTrue(false, string.Format("未対応の値です {0}", type));
                    break;
            }
        }

        public int GetTotalStatusParameter(Constants.StatusParameterType type)
        {
            switch (type)
            {
                case Constants.StatusParameterType.HitPoint:
                    return this.Base.HitPoint;
                case Constants.StatusParameterType.Strength:
                    return this.TotalStrength;
                case Constants.StatusParameterType.Defense:
                    return this.TotalDefense;
                case Constants.StatusParameterType.Sympathy:
                    return this.TotalSympathy;
                case Constants.StatusParameterType.Nega:
                    return this.TotalNega;
                case Constants.StatusParameterType.Speed:
                    return this.TotalSpeed;
                default:
                    Assert.IsTrue(false, string.Format("未対応の値です {0}", type));
                    return 0;
            }
        }
    }
}
