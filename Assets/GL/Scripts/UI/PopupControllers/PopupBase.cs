using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.UI.PopupControllers
{
    /// <summary>
    /// ポップアップの抽象クラス
    /// </summary>
    public abstract class PopupBase : MonoBehaviour, IPopup
    {
        protected Subject<int> submit = new Subject<int>();
        public IObservable<int> SubmitAsObservable() => this.submit;

        protected virtual void OnDestroy()
        {
            this.submit.OnCompleted();
        }

        public virtual void Open()
        {
        }

        public virtual void Close()
        {
            Destroy(this.gameObject);
        }

        /// <summary>
        /// サブミットされたら閉じるイベントを仕込む
        /// </summary>
        public PopupBase CloseOnSubmit()
        {
            this.submit
                .SubscribeWithState(this, (_, _this) => PopupManager.Close(_this))
                .AddTo(this);

            return this;
        }

        protected void OnClickSubmit(Button button, int value)
        {
            button.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) => _this.submit.OnNext(value))
                .AddTo(this);
        }
    }
}
