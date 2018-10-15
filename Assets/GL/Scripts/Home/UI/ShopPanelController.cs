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
        private StringAsset.Finder buyPopupTitle;

        [SerializeField]
        private StringAsset.Finder buyPopupMessage;

        [SerializeField]
        private StringAsset.Finder buyPopupButtonName;

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
            popup.Submit
                .SubscribeWithState2(this, weapon, (isDecide, _this, _weapon) =>
                {
                    // TODO: お金消費処理
                    if(isDecide)
                    {
                        _this.BuyWeapon(_weapon);
                    }
                    else
                    {
                        PopupController.Close();
                    }
                })
                .AddTo(this);
        }

        private void BuyWeapon(Battle.Weapon weapon)
        {
            UserData.Instance.AddWeapon(weapon);
            UserData.Instance.Save();

            PopupController.Close();
            PopupController.ShowBasicPopup(this.buyPopupTitle.Get, this.buyPopupMessage.Get, this.buyPopupButtonName.Get)
                .Submit
                .Subscribe(_ => PopupController.Close());
        }
    }
}
