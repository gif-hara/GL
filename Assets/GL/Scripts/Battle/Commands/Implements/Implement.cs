using System;
using System.Collections.Generic;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using GL.Scripts.Events.Battle;
using HK.Framework.EventSystems;
using UnityEngine.Assertions;

namespace GL.Scripts.Battle.Commands.Implements
{
    /// <summary>
    /// 実際にゲームで使用するコマンドの抽象クラス
    /// </summary>
    public abstract class Implement<T> : IImplement where T : CommandParameter
    {
        protected T parameter;
        
        public string Name { get { return this.parameter.Name.Get; } }

        public Constants.TargetPartyType TargetPartyType { get { return this.parameter.TargetPartyType; } }
        
        public Constants.TargetType TargetType { get { return this.parameter.TargetType; } }
        
        public Constants.StatusParameterType TargetStatusParameterType { get { return this.parameter.TargetStatusParameterType; } }

        public abstract Constants.CommandType CommandType { get; }

        protected Implement(T parameter)
        {
            this.parameter = parameter;
        }

        public virtual void Invoke(Character invoker)
        {
            if (this.CanRecord)
            {
                BattleManager.Instance.InvokedCommandResult.InvokedCommand = this;
            }
        }

        /// <summary>
        /// <see cref="InvokedCommandResult"/>に登録可能か返す
        /// </summary>
        protected bool CanRecord
        {
            get { return this.parameter.Postprocess == Constants.PostprocessCommand.EndTurn; }
        }

        /// <summary>
        /// ターゲットリストを返す
        /// </summary>
        protected List<Character> GetTargets(Character invoker, bool takeDamage)
        {
            return BattleManager.Instance.Parties
                .GetFromTargetPartyType(invoker, this.TargetPartyType)
                .GetTargets(invoker, this.TargetType, c => c.StatusController.GetTotalParameter(this.TargetStatusParameterType), takeDamage);
        }

        public Action Postprocess(Character invoker)
        {
            switch (this.parameter.Postprocess)
            {
                case Constants.PostprocessCommand.EndTurn:
                    return () => this.OnEndTurn(invoker);
                case Constants.PostprocessCommand.CompleteEndTurnEvent:
                    return this.OnCompleteEndTurnEvent;
                default:
                    Assert.IsTrue(false, string.Format("未対応の値です {0}", this.parameter.Postprocess));
                    return null;
            }
        }

        private void OnEndTurn(Character invoker)
        {
            BattleManager.Instance.EndTurn(invoker);
        }

        private void OnCompleteEndTurnEvent()
        {
            Broker.Global.Publish(CompleteEndTurnEvent.Get());
        }
    }
}
