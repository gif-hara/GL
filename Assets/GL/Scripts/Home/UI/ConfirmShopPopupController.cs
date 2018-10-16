using GL.UI.PopupControllers;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// ショップのアイテム購入確認ポップアップを制御するクラス
    /// </summary>
    public sealed class ConfirmShopPopupController : PopupBase
    {
        [SerializeField]
        private Button decide;

        [SerializeField]
        private Button cancel;

        void Start()
        {
            this.decide.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) => _this.submit.OnNext(1))
                .AddTo(this);
                
            this.cancel.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) => _this.submit.OnNext(0))
                .AddTo(this);
        }
    }
}
