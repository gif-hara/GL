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
        public int HitPointMax { get { return this.Blueprint.Status.Parameter.HitPoint; } }

        public int HitPoint { set { this.Base.Parameter.HitPoint = value; } get { return this.Base.Parameter.HitPoint; } }

        /// <summary>
        /// 死亡しているか返す
        /// </summary>
        public bool IsDead { get { return this.HitPoint <= 0; } }

        public int TotalStrength { get { return this.Base.Parameter.Strength + this.Dynamic.Parameter.Strength + this.Accessory.Parameter.Strength; } }

        public int TotalDefense { get { return this.Base.Parameter.Defense + this.Dynamic.Parameter.Defense + this.Accessory.Parameter.Defense; } }
        
        public int TotalSympathy { get { return this.Base.Parameter.Sympathy + this.Dynamic.Parameter.Sympathy + this.Accessory.Parameter.Sympathy; } }
        
        public int TotalNega { get { return this.Base.Parameter.Nega + this.Dynamic.Parameter.Nega + this.Accessory.Parameter.Nega; } }
        
        public int TotalSpeed { get { return this.Base.Parameter.Speed + this.Dynamic.Parameter.Speed + this.Accessory.Parameter.Speed; } }
        
        public float TotalPoison { get { return this.Base.Resistance.Poison + this.Dynamic.Resistance.Poison + this.Accessory.Resistance.Poison; } }

        public float TotalParalysis { get { return this.Base.Resistance.Paralysis + this.Dynamic.Resistance.Paralysis + this.Accessory.Resistance.Paralysis; } }

        public float TotalSleep { get { return this.Base.Resistance.Sleep + this.Dynamic.Resistance.Sleep + this.Accessory.Resistance.Sleep; } }

        public float TotalConfuse { get { return this.Base.Resistance.Confuse + this.Dynamic.Resistance.Confuse + this.Accessory.Resistance.Confuse; } }

        public float TotalBerserk { get { return this.Base.Resistance.Berserk + this.Dynamic.Resistance.Berserk + this.Accessory.Resistance.Berserk; } }

        public CharacterStatusController(Blueprint blueprint)
        {
            this.Blueprint = blueprint;
            this.Base = new CharacterStatus(this.Blueprint);
            this.Dynamic = new CharacterStatus();
            this.Accessory = new CharacterStatus();
            this.Commands = this.Blueprint.Commands.Select(x => x.Create()).ToArray();
            this.Wait = 0.0f;
        }

        public void AddToDynamic(Constants.StatusParameterType type, int value)
        {
            this.Add(this.Dynamic, type, value);
        }

        public void AddToAccessory(Constants.StatusParameterType type, int value)
        {
            this.Add(this.Accessory, type, value);
        }

        public void Add(CharacterStatus status, Constants.StatusParameterType type, int value)
        {
            switch (type)
            {
                case Constants.StatusParameterType.Strength:
                    status.Parameter.Strength += value;
                    break;
                case Constants.StatusParameterType.Defense:
                    status.Parameter.Defense += value;
                    break;
                case Constants.StatusParameterType.Sympathy:
                    status.Parameter.Sympathy += value;
                    break;
                case Constants.StatusParameterType.Nega:
                    status.Parameter.Nega += value;
                    break;
                case Constants.StatusParameterType.Speed:
                    status.Parameter.Speed += value;
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
                    return this.HitPoint;
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
