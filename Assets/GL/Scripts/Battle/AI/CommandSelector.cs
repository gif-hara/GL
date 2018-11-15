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
    /// コマンドを選択するクラス
    /// </summary>
    [Serializable]
    public sealed class CommandSelector
    {
        public enum TargetType
        {
            Random,
        }

        [SerializeField]
        private Condition[] conditions;

        [SerializeField]
        private CommandRecord commandRecord;

        [SerializeField]
        private TargetType targetType;

        public bool Suitable(Character invoker)
        {
            foreach (var c in this.conditions)
            {
                if (c.Suitable(invoker))
                {
                    return true;
                }
            }

            return false;
        }

        public void Invoke(Character invoker)
        {
            var command = this.commandRecord.Create();
            Broker.Global.Publish(SelectedCommand.Get(invoker, command));
            if(command.TargetType.IsSelectType())
            {
                var targets = command.GetTargets(invoker);
                var target = targets[UnityEngine.Random.Range(0, targets.Length)];
                Broker.Global.Publish(SelectedTargets.Get(invoker, command, new Character[] { target }));
            }
        }
    }
}
