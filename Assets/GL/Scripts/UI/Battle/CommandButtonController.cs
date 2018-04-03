using UnityEngine;
using UnityEngine.UI;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Commands.Impletents;
using HK.Framework.EventSystems;
using HK.GL.Events.Battle;
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
