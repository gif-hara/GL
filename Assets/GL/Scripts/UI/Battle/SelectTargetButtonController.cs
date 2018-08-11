using UnityEngine;
using UnityEngine.UI;
using GL.Battle.CharacterControllers;
using GL.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using GL.Battle.Commands.Bundle;

namespace HK.GL.UI.Battle
{
    /// <summary>
    /// ターゲットを選択するボタンを制御する
    /// </summary>
    public sealed class SelectTargetButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private Text text;

        public void SetProperty(Character invoker, Implement command, Character target)
        {
            this.text.text = target.StatusController.Name;

            this.button.OnClickAsObservable()
                .First()
                .TakeUntil(Broker.Global.Receive<SelectedTargets>())
                .Subscribe(_ => Broker.Global.Publish(SelectedTargets.Get(invoker, command, new Character[]{ target })))
                .AddTo(this);
        }
    }
}
