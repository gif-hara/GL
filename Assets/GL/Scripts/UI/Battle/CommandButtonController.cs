using UnityEngine;
using UnityEngine.UI;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Commands.Implements;
using GL.Scripts.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;

namespace HK.GL.UI.Battle
{
    /// <summary>
    /// コマンドボタンを制御する
    /// </summary>
    public sealed class CommandButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private Text text;

        public void SetProperty(Character invoker, IImplement implement)
        {
            this.text.text = implement.Name;

            this.button.OnClickAsObservable()
                .First()
                .TakeUntil(Broker.Global.Receive<InvokeCommand>())
                .Subscribe(_ => Broker.Global.Publish(InvokeCommand.Get(invoker, implement)))
                .AddTo(this);
        }
    }
}
