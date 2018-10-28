using System;
using GL.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Battle.UI
{
    /// <summary>
    /// コマンド実行をUIで表示するクラス
    /// </summary>
    public sealed class CommandNotifyUIController : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private Text text;

        [SerializeField]
        private float visibleTime;

        void Awake()
        {
            Broker.Global.Receive<SelectedTargets>()
                .SubscribeWithState(this, (x, _this) => _this.VisibleCommandName(x.Command.Name))
                .AddTo(this);

            this.Hidden();
        }

        private void VisibleCommandName(string commandName)
        {
            this.canvasGroup.alpha = 1.0f;
            this.text.text = commandName;
            Observable.Timer(TimeSpan.FromSeconds(this.visibleTime))
                .TakeUntil(Broker.Global.Receive<SelectedTargets>())
                .SubscribeWithState(this, (_, _this) => _this.Hidden())
                .AddTo(this);
        }

        private void Hidden()
        {
            this.canvasGroup.alpha = 0.0f;
        }
    }
}
