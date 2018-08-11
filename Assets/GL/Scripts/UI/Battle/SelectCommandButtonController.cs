using UnityEngine;
using UnityEngine.UI;
using GL.Battle.CharacterControllers;
using GL.Scripts.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using GL.Battle.Commands.Bundle;

namespace HK.GL.UI.Battle
{
    /// <summary>
    /// コマンドを選択するボタンを制御する
    /// </summary>
    public sealed class SelectCommandButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private Text text;

        public void SetProperty(Character invoker, Implement implement)
        {
            this.text.text = implement.Name;

            this.button.OnClickAsObservable()
                .First()
                .TakeUntil(Broker.Global.Receive<SelectedCommand>())
                .Subscribe(_ => Broker.Global.Publish(SelectedCommand.Get(invoker, implement)))
                .AddTo(this);
        }
    }
}
