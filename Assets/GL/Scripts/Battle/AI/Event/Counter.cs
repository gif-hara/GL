using System.Collections.Generic;
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
    /// カウンター攻撃を行うイベント
    /// </summary>
    [CreateAssetMenu(menuName = "GL/AI/Event/Counter")]
    public sealed class Counter : Event
    {
        public enum TargetType
        {
            /// <summary>
            /// 影響を与えたキャラクター
            /// </summary>
            AffectedCharacter,

            /// <summary>
            /// コマンドを実行したキャラクター
            /// </summary>
            InvokedCharacter,
        }

        [SerializeField]
        private CommandRecord commandRecord;

        [SerializeField]
        private TargetType targetType;

        public override void Invoke(Character invoker)
        {
            BattleManager.Instance.EndTurnEvents.Enqueue(() =>
            {
                var command = this.commandRecord.Create();
                Broker.Global.Publish(SelectedCommand.Get(invoker, command));
                if (command.TargetType.IsSelectType())
                {
                    Broker.Global.Publish(SelectedTargets.Get(invoker, command, this.GetTargets(invoker)));
                }
            });
        }

        private Character[] GetTargets(Character invoker)
        {
            var result = new List<Character>();
            switch(this.targetType)
            {
                case TargetType.AffectedCharacter:
                    result.Add(invoker.AIController.AffectedCharacter);
                    break;
                case TargetType.InvokedCharacter:
                    result.Add(BattleManager.Instance.InvokedCommandResult.InvokedCharacter);
                    break;
                default:
                    Assert.IsTrue(false, $"{this.targetType}は未対応の値です");
                    break;
            }

            return result.ToArray();
        }

#if UNITY_EDITOR
        public Counter Set(CommandRecord commandRecord, TargetType targetType)
        {
            this.commandRecord = commandRecord;
            this.targetType = targetType;

            return this;
        }
#endif
    }
}
