using DG.Tweening;
using GL.Events.Home;
using GL.User;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// ホームのプレイヤーボタンを制御するクラス
    /// </summary>
    public sealed class PlayerButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button;
        public Button Button { get { return button; } }

        [SerializeField]
        private Text text;

        [SerializeField]
        private float changeTargetScaleEndValue;

        [SerializeField]
        private float changeTargetScaleDuration;

        [SerializeField]
        private float changePlayerRotateZEndValue;

        [SerializeField]
        private float changePlayerRotateZDurationMin;

        [SerializeField]
        private float changePlayerRotateZDurationMax;

        public readonly CompositeDisposable Disposable = new CompositeDisposable();

        private Tween changeTween;

        public void Setup(Player player)
        {
            this.text.text = player.Blueprint.CharacterName;
            this.Disposable.Clear();

            Broker.Global.Receive<StartPlayerChange>()
                .SubscribeWithState2(this, player, (x, _this, _player) =>
                {
                    if (x.Target == _player)
                    {
                        _this.changeTween = _this.CreateChangeTargetTweener();
                    }
                    else
                    {
                        _this.changeTween = _this.CreateChangePlayerTweener();
                    }
                })
                .AddTo(this)
                .AddTo(this.Disposable);

            Broker.Global.Receive<CompletePlayerChange>()
                .SubscribeWithState(this, (_, _this) => _this.EndTween())
                .AddTo(this)
                .AddTo(this.Disposable);
        }

        /// <summary>
        /// 交代したい側のアニメーションを返す
        /// </summary>
        private Tween CreateChangeTargetTweener()
        {
            return this.transform.DOScale(Vector3.one * this.changeTargetScaleEndValue, this.changeTargetScaleDuration)
                .SetLoops(-1, LoopType.Yoyo);
        }

        /// <summary>
        /// 交代される側のアニメーションを返す
        /// </summary>
        private Tween CreateChangePlayerTweener()
        {
            var duration = Random.Range(this.changePlayerRotateZDurationMin, this.changePlayerRotateZDurationMax);
            return DOTween.Sequence()
                .Append(this.transform.DOLocalRotate(Vector3.forward * this.changePlayerRotateZEndValue, duration))
                .Append(this.transform.DOLocalRotate(Vector3.forward * -this.changePlayerRotateZEndValue, duration))
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void EndTween()
        {
            if(this.changeTween == null)
            {
                return;
            }

            this.changeTween.Kill();
            this.changeTween = null;
            this.transform.localScale = Vector3.one;
            this.transform.localRotation = Quaternion.identity;
        }
    }
}
