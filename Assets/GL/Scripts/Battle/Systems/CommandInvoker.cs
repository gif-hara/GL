using GL.Scripts.Battle.Commands.Implements;
using GL.Scripts.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;

namespace GL.Scripts.Battle.Systems
{
    /// <summary>
    /// 行動するキャラクターのコマンドを実行するヤーツ
    /// </summary>
    public sealed class CommandInvoker : MonoBehaviour
    {
        /// <summary>
        /// 最後に実行されたコマンド
        /// </summary>
        public IImplement LastCommand { get; private set; }

        void Awake()
        {
            Broker.Global.Receive<InvokeCommand>()
                .Subscribe(this.OnInvokeCommand)
                .AddTo(this);
        }

        private void OnInvokeCommand(InvokeCommand eventData)
        {
            this.LastCommand = eventData.Command;
            var character = eventData.Invoker;
            eventData.Command.Invoke(character);
        }
    }
}
