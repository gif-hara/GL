using GL.Scripts.Battle.Commands.Implements;
using GL.Scripts.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;

namespace GL.Scripts.Battle.Systems
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
        }

        private void OnSelectedCommand(SelectedCommand eventData)
        {
            if(eventData.Command.TargetType == Constants.TargetType.Select)
            {
                Debug.LogWarning("Selectは未実装");
                return;
            }
            var command = eventData.Command;
            var character = eventData.Invoker;
            command.Invoke(character, command.GetTargets(character));
        }
    }
}
