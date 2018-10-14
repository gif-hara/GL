using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// ショップのリスト要素を制御するクラス
    /// </summary>
    public sealed class ShopElementController : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private Image icon;

        [SerializeField]
        private Text title;

        [SerializeField]
        private Text description;

        private ShopPanelController shopPanelController;

        public void Setup(ShopPanelController shopPanelController, Battle.Weapon weapon)
        {
            this.title.text = weapon.WeaponName;
            this.description.text = "";

            this.button.OnClickAsObservable()
                .SubscribeWithState2(shopPanelController, weapon, (_, _shopPanelController, _weapon) =>
                {
                    _shopPanelController.ShowConfirmPopup(_weapon);
                })
                .AddTo(this);
        }
    }
}
