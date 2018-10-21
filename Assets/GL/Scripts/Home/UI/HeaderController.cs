using GL.User;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// ホームのヘッダーUIを制御するクラス
    /// </summary>
    public sealed class HeaderController : MonoBehaviour
    {
        [SerializeField]
        private Text userName;

        [SerializeField]
        private Text gold;

        [SerializeField]
        private Text experience;

        void Start()
        {
            var u = UserData.Instance;
            this.userName.text = u.UserName;
            u.Wallet.Gold.ReactiveProperty
                .SubscribeWithState(this, (x, _this) => _this.gold.text = x.ToString())
                .AddTo(this);
            u.Wallet.Experience.ReactiveProperty
                .SubscribeWithState(this, (x, _this) => _this.experience.text = x.ToString())
                .AddTo(this);
        }
    }
}
