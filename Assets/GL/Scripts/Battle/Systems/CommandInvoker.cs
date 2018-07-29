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
            var character = eventData.Invoker;
            eventData.Command.Invoke(character);
        }
    }
}
