using System;
using GL.Battle.CharacterControllers;
using GL.Database;
using GL.Events.Battle;
using GL.Extensions;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.AIControllers
{
    /// <summary>
    /// 実際にコマンドを実行するAI要素
    /// </summary>
    [Serializable]
    public sealed class InvokeCommand
    {
        public enum TargetType
        {
            Random,
        }

        [SerializeField]
        private CommandRecord commandRecord;

        [SerializeField]
        private TargetType targetType;

        /// <summary>
        /// コマンドを実行する
        /// </summary>
        public void Invoke(Character invoker)
        {
            var command = this.commandRecord.Create();
            Broker.Global.Publish(SelectedCommand.Get(invoker, command));
            if (command.TargetType.IsSelectType())
            {
                var targets = command.GetTargets(invoker);
                var target = targets[UnityEngine.Random.Range(0, targets.Length)];
                Broker.Global.Publish(SelectedTargets.Get(invoker, command, new Character[] { target }));
            }
        }

#if UNITY_EDITOR
        public InvokeCommand Set(CommandRecord commandRecord, TargetType targetType)
        {
            this.commandRecord = commandRecord;
            this.targetType = targetType;

            return this;
        }
#endif
    }
}
