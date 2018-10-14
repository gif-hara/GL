using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// ショップのアイテム購入確認ポップアップを制御するクラス
    /// </summary>
    public sealed class ConfirmShopPopupController : MonoBehaviour
    {
        public Subject<bool> Submit { get; private set; } = new Subject<bool>();

        [SerializeField]
        private Button decide;

        [SerializeField]
        private Button cancel;

        void Start()
        {
            this.decide.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) => _this.InvokeSubmit(true))
                .AddTo(this);
                
            this.cancel.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) => _this.InvokeSubmit(false))
                .AddTo(this);
        }

        private void InvokeSubmit(bool isDecide)
        {
            this.Submit.OnNext(isDecide);
            this.Submit.OnCompleted();
        }
    }
}
