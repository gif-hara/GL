using System.Linq;
using GL.MasterData;
using GL.UI.PopupControllers;
using GL.User;
using HK.Framework.Text;
using HK.GL.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using static GL.Constants;

namespace GL.Home.UI
{
    /// <summary>
    /// ショップのパネルを制御するクラス
    /// </summary>
    public sealed class ShopPanelController : MonoBehaviour
    {
        [SerializeField]
        private GameObject topRoot;

        [SerializeField]
        private GameObject listRoot;

        [SerializeField]
        private RectTransform listContents;

        [SerializeField]
        private ShopElementController shopElementControllerPrefab;

        [SerializeField]
        private ConfirmShopPopupController confirmShopPopupController;

        [SerializeField]
        private SimplePopupStrings buyPopup;

        [SerializeField]
        private SimplePopupStrings notBuyFromGoldPopup;

        /// <summary>
        /// <paramref name="weaponType"/>に対応した武器リストを表示する
        /// </summary>
        public void ShowWeaponList(WeaponType weaponType)
        {
            this.topRoot.SetActive(false);
            this.listRoot.SetActive(true);

            var targetWeapons = Database.Weapon.List.Where(w => w.WeaponType == weaponType);
            targetWeapons.ForEach(w =>
            {
                var element = Instantiate(this.shopElementControllerPrefab, this.listContents, false);
                element.Setup(this, w);
            });
        }

        /// <summary>
        /// トップ画面を表示する
        /// </summary>
        public void ShowTop()
        {
            this.topRoot.SetActive(true);
            this.listRoot.SetActive(false);
            for (var i = 0; i < this.listContents.childCount; i++)
            {
                Destroy(this.listContents.GetChild(i).gameObject);
            }
        }

        public void ShowConfirmPopup(Battle.Weapon weapon)
        {
            var popup = PopupController.Show(this.confirmShopPopupController);
            popup.SubmitAsObservable()
                .SubscribeWithState2(this, weapon, (index, _this, _weapon) =>
                {
                    var isDecide = index > 0;
                    if(isDecide)
                    {
                        _this.BuyWeapon(_weapon);
                    }
                    else
                    {
                        PopupController.Close();
                    }
                })
                .AddTo(popup);
        }

        private void BuyWeapon(Battle.Weapon weapon)
        {
            PopupController.Close();
            var userData = UserData.Instance;
            if(!userData.Wallet.IsEnoughGold(weapon.Price))
            {
                this.notBuyFromGoldPopup.Show().SubmitAsObservable()
                    .Subscribe(_ => PopupController.Close());
                return;
            }

            userData.AddWeapon(weapon);
            userData.Wallet.PayFromGold(weapon.Price);
            userData.Save();

            this.buyPopup.Show().SubmitAsObservable()
                .Subscribe(_ => PopupController.Close());
        }
    }
}
