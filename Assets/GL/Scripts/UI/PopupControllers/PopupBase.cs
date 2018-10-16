using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

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
    }
}
