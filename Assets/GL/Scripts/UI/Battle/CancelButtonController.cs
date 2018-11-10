using GL.Events.Battle;
using GL.Events.Home;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Battle.UI
{
    /// <summary>
    /// バトルのキャンセルボタンを制御するクラス
    /// </summary>
    public sealed class CancelButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        void Start()
        {
            this.button.OnClickAsObservable()
                .Subscribe(_ => Broker.Global.Publish(ClickedFooterCancelButton.Get()))
                .AddTo(this);
                
            Broker.Global.Receive<StartSelectCommand>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.gameObject.SetActive(false);
                })
                .AddTo(this);

            Broker.Global.Receive<StartSelectTarget>()
                .Where(x => x.Character.CharacterType == Constants.CharacterType.Player)
                .SubscribeWithState(this, (_, _this) => _this.gameObject.SetActive(true))
                .AddTo(this);

            Broker.Global.Receive<SelectedTargets>()
                .SubscribeWithState(this, (_, _this) => _this.gameObject.SetActive(false))
                .AddTo(this);

            this.gameObject.SetActive(false);
        }
    }
}
