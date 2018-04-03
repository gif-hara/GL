using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using UnityEngine.UI;
using HK.GL.Battle;
using UnityEngine.EventSystems;
using System;
using HK.Framework.EventSystems;
using HK.GL.Events;
using HK.GL.Events.Battle;
using UniRx.Triggers;
using UniRx;

namespace HK.GL.UI.Battle
{
    /// <summary>
    /// コマンドボタンを制御するヤーツ
    /// </summary>
    public sealed class CommandButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private Text text;

        public void SetProperty(Character invoker, ICommand command)
        {
            this.text.text = command.Name;

            this.button.OnClickAsObservable()
                .First()
                .TakeUntil(Broker.Global.Receive<InvokeCommand>())
                .Subscribe(_ => Broker.Global.Publish(InvokeCommand.Get(invoker, command)))
                .AddTo(this);
        }
    }
}
