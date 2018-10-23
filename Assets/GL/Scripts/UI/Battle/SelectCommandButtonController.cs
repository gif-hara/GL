using UnityEngine;
using UnityEngine.UI;
using GL.Battle.CharacterControllers;
using GL.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using GL.Battle.Commands.Bundle;
using HK.Framework.Text;

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

        [SerializeField]
        private Image lockImage;

        [SerializeField]
        private Text remainingTurn;

        [SerializeField]
        private StringAsset.Finder remainingTurnFormat;

        public void SetProperty(Character invoker, Implement implement)
        {
            this.text.text = implement.Name;

            var canInvoke = implement.CanInvoke;
            this.button.interactable = canInvoke;
            this.lockImage.enabled = !canInvoke;
            this.remainingTurn.enabled = !canInvoke;
            this.remainingTurn.text = this.remainingTurnFormat.Format(implement.CurrentChargeTurn.ToString(), implement.ChargeTurn.ToString());

            this.button.OnClickAsObservable()
                .First()
                .TakeUntil(Broker.Global.Receive<SelectedCommand>())
                .Subscribe(_ => Broker.Global.Publish(SelectedCommand.Get(invoker, implement)))
                .AddTo(this);
        }
    }
}
