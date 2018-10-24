using GL.Battle;
using GL.Database;
using GL.Events.Battle;
using HK.Framework.EventSystems;
using HK.GL.Extensions;
using UniRx;
using UnityEngine;

namespace GL.Battle.CharacterControllers
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
        /// 状態異常の孤軍奮闘によって加算されるステータス
        /// </summary>
        public CharacterStatus OnSoldier { private set; get; }

        /// <summary>
        /// 使用可能なコマンド
        /// </summary>
        public Commands.Bundle.Implement[] Commands { private set; get; }

        /// <summary>
        /// 待機した量
        /// </summary>
        public float Wait { set; get; }

        /// <summary>
        /// 元となる設計図
        /// </summary>
        public CharacterRecord CharacterRecord { private set; get; }

        public Character Character { get; private set; }

        public string Name { get { return this.Base.Name; } }

        /// <summary>
        /// ヒットポイント最大値
        /// </summary>
        public int HitPointMax { get; private set; }

        public int HitPoint
        {
            set
            {
                this.Base.Parameter.HitPoint = Mathf.Max(value, 0);
                this.PublishModifiedStatus();
            }
            get
            {
                return this.Base.Parameter.HitPoint;
            }
        }

        public float HitPointRate => (float)this.HitPoint / this.HitPointMax;

        /// <summary>
        /// 死亡しているか返す
        /// </summary>
        public bool IsDead { get { return this.HitPoint <= 0; } }

        public CharacterStatusController(Character character, CharacterRecord blueprint, Commands.Bundle.Implement[] commands, int level)
        {
            this.Character = character;
            this.CharacterRecord = blueprint;
            this.Base = new CharacterStatus(this.CharacterRecord, level);
            this.Dynamic = new CharacterStatus();
            this.Accessory = new CharacterStatus();
            this.OnSoldier = new CharacterStatus();
            this.Commands = commands;
            this.Wait = 0.0f;
            this.HitPointMax = this.Base.Parameter.HitPoint;
        }

        public void AddParameterToDynamic(Constants.StatusParameterType type, int value)
        {
            this.Dynamic.Parameter.Add(type, value);
            this.PublishModifiedStatus();
        }

        public void AddParameterToAccessory(Constants.StatusParameterType type, int value)
        {
            this.Accessory.Parameter.Add(type, value);
            this.PublishModifiedStatus();
        }

        public void AddResistanceToAccessory(Constants.StatusAilmentType type, float value)
        {
            this.Accessory.Resistance.Add(type, value);
            this.PublishModifiedStatus();
        }

        public int GetTotalParameter(Constants.StatusParameterType type)
        {
            return this.Base.Parameter.Get(type) +
                   this.Dynamic.Parameter.Get(type) +
                   this.Accessory.Parameter.Get(type) +
                   this.OnSoldier.Parameter.Get(type);
        }

        public float GetTotalResistance(Constants.StatusAilmentType type)
        {
            return this.Base.Resistance.Get(type) +
                   this.Dynamic.Resistance.Get(type) +
                   this.Accessory.Resistance.Get(type) +
                   this.OnSoldier.Resistance.Get(type);
        }

        public void AddChargeTurn(int value)
        {
            this.Commands.ForEach(c => c.AddChargeTurn(value));
        }

        private void PublishModifiedStatus()
        {
            Broker.Global.Publish(ModifiedStatus.Get(this.Character));
        }
    }
}
