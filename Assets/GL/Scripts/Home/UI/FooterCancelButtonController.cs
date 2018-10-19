using GL.Events.Home;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// フッターのキャンセルボタンを制御するクラス
    /// </summary>
    public sealed class FooterCancelButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        void Start()
        {
            this.button.OnClickAsObservable()
                .Subscribe(_ => Broker.Global.Publish(ClickedFooterCancelButton.Get()))
                .AddTo(this);
        }
    }
}
