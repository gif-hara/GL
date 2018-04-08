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

        public CharacterStatusController(Blueprint blueprint)
        {
            this.Blueprint = blueprint;
            this.Base = new CharacterStatus(this.Blueprint);
            this.Dynamic = new CharacterStatus();
            this.Accessory = new CharacterStatus();
            this.Commands = this.Blueprint.Commands.Select(x => x.Create()).ToArray();
            this.Wait = 0.0f;
        }

        public void AddParameterToDynamic(Constants.StatusParameterType type, int value)
        {
            this.Dynamic.Parameter.Add(type, value);
        }

        public void AddParameterToAccessory(Constants.StatusParameterType type, int value)
        {
            this.Accessory.Parameter.Add(type, value);
        }

        public void AddResistanceToAccessory(Constants.StatusAilmentType type, float value)
        {
            this.Accessory.Resistance.Add(type, value);
        }

        public int GetTotalParameter(Constants.StatusParameterType type)
        {
            return this.Base.Parameter.Get(type) +
                   this.Dynamic.Parameter.Get(type) +
                   this.Accessory.Parameter.Get(type);
        }

        public float GetTotalResistance(Constants.StatusAilmentType type)
        {
            return this.Base.Resistance.Get(type) +
                   this.Dynamic.Resistance.Get(type) +
                   this.Accessory.Resistance.Get(type);
        }
    }
}
