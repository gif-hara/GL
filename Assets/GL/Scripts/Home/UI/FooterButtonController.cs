using GL.Events.Home;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// フッターボタンを制御するクラス
    /// </summary>
    public sealed class FooterButtonController : MonoBehaviour
    {
        [SerializeField]
        private MovableAreaController movableAreaController;

        [SerializeField]
        private int rootIndex;
        
        void Awake()
        {
            var button = this.GetComponent<Button>();
            button.OnClickAsObservable()
                .Where(_ => button.isActiveAndEnabled)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.movableAreaController.ChangeRoot(_this.rootIndex);
                })
                .AddTo(this);
            Broker.Global.Receive<ChangeFooter>()
                .SubscribeWithState(button, (x, _button) => _button.interactable = x.Mode == FooterController.Mode.Default)
                .AddTo(this);
        }
    }
}
