﻿using System.Collections.Generic;
using GL.Scripts.Battle.Systems;
using HK.Framework.EventSystems;
using HK.GL.Events.Battle;
using UniRx;

namespace GL.Scripts.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターの状態異常を制御するクラス
    /// </summary>
    public sealed class CharacterAilmentController
    {
        /// <summary>
        /// 現在かかっている状態異常
        /// </summary>
        private readonly List<StatusAilmentElement> elements = new List<StatusAilmentElement>();

        public CharacterAilmentController(Character character)
        {
            Broker.Global.Receive<EndTurn>()
                .Where(x => x.Character == character)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.elements.ForEach(e => e.EndTurn());
                })
                .AddTo(character);
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
            
            this.elements.Add(new StatusAilmentElement(remainingTurn, type, this));
        }
        
        /// <summary>
        /// <paramref name="type"/>にかかっているか返す
        /// </summary>
        public bool Find(Constants.StatusAilmentType type)
        {
            return this.elements.FindIndex(e => e.Type == type) != -1;
        }

        public class StatusAilmentElement
        {
            public int RemainingTurn { get; private set; }

            public Constants.StatusAilmentType Type { get; private set; }

            private readonly CharacterAilmentController controller;

            public StatusAilmentElement(int remainingTurn, Constants.StatusAilmentType type, CharacterAilmentController controller)
            {
                this.RemainingTurn = remainingTurn;
                this.Type = type;
                this.controller = controller;
            }

            public void EndTurn()
            {
                this.RemainingTurn--;
                if (this.RemainingTurn <= 0)
                {
                    this.controller.elements.Remove(this);
                }
            }
        }
    }
}