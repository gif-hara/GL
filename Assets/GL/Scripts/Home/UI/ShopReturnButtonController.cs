using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// ショップの戻るボタンを制御するクラス
    /// </summary>
    public sealed class ShopReturnButtonController : MonoBehaviour
    {
        [SerializeField]
        private ShopPanelController shopPanelController;

        [SerializeField]
        private Button button;

        void Start()
        {
            this.button.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) => _this.shopPanelController.ShowTop())
                .AddTo(this);
        }
    }
}
