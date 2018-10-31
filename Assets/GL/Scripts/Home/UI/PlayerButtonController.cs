using DG.Tweening;
using GL.Events.Home;
using GL.Tweens;
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
        private EmphasisScaleTween scaleTween;

        [SerializeField]
        private TargetableTween changePlayerTween;

        public readonly CompositeDisposable Disposable = new CompositeDisposable();

        private Tween changeTween;

        public void Setup(Player player)
        {
            this.text.text = player.CharacterRecord.CharacterName;
            this.Disposable.Clear();

            Broker.Global.Receive<StartPlayerChange>()
                .SubscribeWithState2(this, player, (x, _this, _player) =>
                {
                    var u = UserData.Instance;
                    if (x.Target == _player)
                    {
                        _this.changeTween = _this.CreateChangeTargetTweener();
                    }
                    else if(u.CurrentParty.Contains(_player) || u.CurrentParty.Contains(x.Target))
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
            return this.scaleTween.Apply(this.transform);
        }

        /// <summary>
        /// 交代される側のアニメーションを返す
        /// </summary>
        private Tween CreateChangePlayerTweener()
        {
            return this.changePlayerTween.Apply(this.transform);
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
