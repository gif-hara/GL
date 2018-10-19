using GL.Events.Home;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using DG.Tweening;

namespace GL.Home.UI
{
    /// <summary>
    /// フッターを制御するクラス
    /// </summary>
    public sealed class FooterController : MonoBehaviour
    {
        public enum Mode
        {
            /// <summary>
            /// 通常モード
            /// </summary>
            Default,

            /// <summary>
            /// キャンセルモード
            /// </summary>
            Cancel,
        }

        [SerializeField]
        private Transform DefaultRoot;

        [SerializeField]
        private Transform CancelRoot;

        [SerializeField]
        private Mode initialMode;

        [SerializeField]
        private float duration;

        [SerializeField]
        private Ease ease;

        private RectTransform cachedTransform;

        private Mode currentMode;

        void Start()
        {
            Broker.Global.Receive<ChangeFooter>()
                .SubscribeWithState(this, (x, _this) => _this.Change(x.Mode))
                .AddTo(this);

            this.cachedTransform = (RectTransform)this.transform;
            this.ChangeImmediate(this.initialMode);
        }

        private void Change(FooterController.Mode mode)
        {
            if(this.currentMode == mode)
            {
                return;
            }
            this.CurrentRoot.DOLocalMoveY(-this.cachedTransform.rect.height, this.duration, true).SetEase(this.ease);
            this.currentMode = mode;
            this.CurrentRoot.DOLocalMoveY(0.0f, this.duration, true).SetEase(this.ease);
        }

        private void ChangeImmediate(FooterController.Mode mode)
        {
            this.currentMode = mode;
            this.ChangeImmediate(this.DefaultRoot, mode == Mode.Default);
            this.ChangeImmediate(this.CancelRoot, mode == Mode.Cancel);
        }

        private void ChangeImmediate(Transform root, bool isEnable)
        {
            var pos = root.localPosition;
            pos.y = isEnable ? 0.0f : -this.cachedTransform.rect.height;
            root.localPosition = pos;
        }

        private Transform CurrentRoot
        {
            get
            {
                switch(this.currentMode)
                {
                    case Mode.Default:
                        return this.DefaultRoot;
                    case Mode.Cancel:
                        return this.CancelRoot;
                    default:
                        Assert.IsTrue(false, $"{this.currentMode}は未対応の値です");
                        return null;
                }
            }
        }
    }
}
