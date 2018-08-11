using GL.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;

namespace GL.Battle.Systems
{
    /// <summary>
    /// 行動するキャラクターのコマンドを実行する
    /// </summary>
    public sealed class CommandInvoker : MonoBehaviour
    {
        void Awake()
        {
            Broker.Global.Receive<SelectedCommand>()
                .Subscribe(this.OnSelectedCommand)
                .AddTo(this);

            Broker.Global.Receive<SelectedTargets>()
                .Subscribe(this.OnSelectedTargets)
                .AddTo(this);
        }

        private void OnSelectedCommand(SelectedCommand eventData)
        {
            var command = eventData.Command;
            var invoker = eventData.Invoker;
            var targets = command.GetTargets(invoker);

            // ターゲットを選択する必要がある場合はStartSelectTargetイベントを飛ばす
            if(eventData.Command.TargetType == Constants.TargetType.Select)
            {
                Broker.Global.Publish(StartSelectTarget.Get(invoker, command, targets));
            }
            // それ以外はコマンド実行
            else
            {
                Broker.Global.Publish(SelectedTargets.Get(invoker, command, targets));
            }
        }

        private void OnSelectedTargets(SelectedTargets eventData)
        {
            eventData.Command.Invoke(eventData.Invoker, eventData.Targets);
        }
    }
}
