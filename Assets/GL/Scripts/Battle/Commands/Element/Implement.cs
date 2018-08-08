using System;
using System.Collections.Generic;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using GL.Scripts.Events.Battle;
using HK.Framework.EventSystems;
using UnityEngine.Assertions;

namespace GL.Scripts.Battle.Commands.Element
{
    /// <summary>
    /// 実際にゲームで使用するコマンドの抽象クラス
    /// </summary>
    public abstract class Implement<T> : IImplement where T : IParameter
    {
        protected T parameter;        
        
        public abstract bool TakeDamage { get; }

        protected Implement(T parameter)
        {
            this.parameter = parameter;
        }

        public abstract void Invoke(Character invoker, Commands.Bundle.Implement bundle, Character[] targets);

        private void OnEndTurn(Character invoker)
        {
            BattleManager.Instance.EndTurn(invoker);
        }

        private void OnCompleteEndTurnEvent()
        {
            Broker.Global.Publish(CompleteEndTurnEvent.Get());
        }

        public Character[] GetTargets(Character invoker)
        {
            throw new NotImplementedException();
        }
    }
}
