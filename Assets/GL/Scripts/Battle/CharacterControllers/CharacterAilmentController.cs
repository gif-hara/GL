using System.Collections.Generic;
using GL.Scripts.Battle.CharacterControllers.StatusAilments;
using GL.Scripts.Battle.Systems;
using GL.Scripts.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;

namespace GL.Scripts.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターの状態異常を制御するクラス
    /// </summary>
    public sealed class CharacterAilmentController
    {
        public Character Character { get; private set; }

        /// <summary>
        /// 現在かかっている状態異常
        /// </summary>
        public readonly List<Element> Elements = new List<Element>();

        public CharacterAilmentController(Character character)
        {
            this.Character = character;
            Broker.Global.Receive<EndTurn>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    if (x.Character == _this.Character)
                    {
                        _this.Elements.ForEach(e => e.EndTurn());
                    }
                    _this.Elements.ForEach(e => e.EndTurnAll());
                })
                .AddTo(this.Character);
        }

        /// <summary>
        /// 状態異常を追加する
        /// </summary>
        /// <remarks>
        /// すでにかかっている場合は何もしない
        /// </remarks>
        public void Add(int remainingTurn, Constants.StatusAilmentType type)
        {
            if (this.Find(type))
            {
                return;
            }
            
            this.Elements.Add(Factory.Create(remainingTurn, type, this));
        }
        
        /// <summary>
        /// <paramref name="type"/>にかかっているか返す
        /// </summary>
        public bool Find(Constants.StatusAilmentType type)
        {
            return this.Elements.FindIndex(e => e.Type == type) != -1;
        }

        /// <summary>
        /// <see cref="Element"/>を返す
        /// </summary>
        public Element Get(Constants.StatusAilmentType type)
        {
            return this.Elements.Find(e => e.Type == type);
        }

        /// <summary>
        /// ダメージを受けた際の処理
        /// </summary>
        public void TakeDamage()
        {
            this.Elements.ForEach(e => e.TakeDamage());
        }

        public void Remove(Element item)
        {
            this.Elements.Remove(item);
            item.OnRemove();
        }
    }
}
