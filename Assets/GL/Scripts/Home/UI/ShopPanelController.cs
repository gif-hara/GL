using System.Linq;
using GL.Database;
using GL.Events.Home;
using GL.UI.PopupControllers;
using GL.User;
using HK.Framework.EventSystems;
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

        [SerializeField]
        private SimplePopupStrings notBuyFromNeedMaterialPopup;

        /// <summary>
        /// <paramref name="weaponType"/>に対応した武器リストを表示する
        /// </summary>
        public void ShowWeaponList(EquipmentType weaponType)
        {
            this.topRoot.SetActive(false);
            this.listRoot.SetActive(true);

            var targetWeapons = UserData.Instance.UnlockElements.Equipments
                .Select(w => MasterData.Equipment.GetById(w))
                .Where(w => w.EquipmentType == weaponType);
            targetWeapons.ForEach(w =>
            {
                var element = Instantiate(this.shopElementControllerPrefab, this.listContents, false);
                element.Setup(this, w);
            });

            Broker.Global.Publish(ChangeFooter.Get(FooterController.Mode.Cancel));
            Broker.Global.Receive<ClickedFooterCancelButton>()
                .Take(1)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.ShowTop();
                    Broker.Global.Publish(ChangeFooter.Get(FooterController.Mode.Default));
                })
                .AddTo(this);
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

        public void ShowConfirmPopup(EquipmentRecord equipment)
        {
            var popup = PopupManager.Show(this.confirmShopPopupController).Setup(equipment);
            popup.SubmitAsObservable()
                .SubscribeWithState2(this, equipment, (index, _this, _weapon) =>
                {
                    var isDecide = index > 0;
                    if(isDecide)
                    {
                        _this.BuyEquipment(_weapon);
                    }
                    else
                    {
                        PopupManager.Close();
                    }
                })
                .AddTo(popup);
        }

        private void BuyEquipment(EquipmentRecord equipment)
        {
            PopupManager.Close();
            var userData = UserData.Instance;

            // ゴールドが足りているか
            if(!userData.Wallet.Gold.IsEnough(equipment.Price))
            {
                this.notBuyFromGoldPopup.Show().CloseOnSubmit();
                return;
            }

            // 必要素材が足りているか
            if(!userData.CanBuyEquipment(equipment))
            {
                this.notBuyFromNeedMaterialPopup.Show().CloseOnSubmit();
                return;
            }

            userData.BuyEquipment(equipment);
            userData.Save();

            this.buyPopup.Show().SubmitAsObservable()
                .Subscribe(_ => PopupManager.Close());
        }
    }
}
