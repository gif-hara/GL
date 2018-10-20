using GL.Events.Home;
using GL.MasterData;
using GL.UI.PopupControllers;
using GL.User;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Home.UI
{
    /// <summary>
    /// 雇用パネルを制御するクラス
    /// </summary>
    public sealed class EmployPanelController : MonoBehaviour
    {
        [SerializeField]
        private Transform listParent;

        [SerializeField]
        private EmployElementController elementPrefab;

        [SerializeField]
        private CharacterDetailsPopupController characterDetailsPopupController;

        [SerializeField]
        private SimplePopupStrings successEmploy;

        [SerializeField]
        private SimplePopupStrings failedEmploy;

        void Start()
        {
            foreach(var c in Database.Character.List)
            {
                var element = Instantiate(this.elementPrefab, this.listParent, false).Setup(c);
                element.Button.OnClickAsObservable()
                    .SubscribeWithState2(this, c, (_, _this, _c) => _this.ShowCharacterDetailsPopup(_c));
            }
        }

        private void ShowCharacterDetailsPopup(Battle.CharacterControllers.Blueprint blueprint)
        {
            var popup = PopupManager.Show(this.characterDetailsPopupController).Setup(blueprint, CharacterDetailsPopupController.Mode.Employ);
            popup
                .SubmitAsObservable()
                .Select(i => (CharacterDetailsPopupController.SubmitType)i)
                .SubscribeWithState3(this, popup, blueprint, (type, _this, _popup, b) =>
                {
                    switch (type)
                    {
                        case CharacterDetailsPopupController.SubmitType.EmployDecide:
                            PopupManager.Close(_popup);
                            var u = UserData.Instance;
                            if(u.Wallet.Gold.IsEnough(b.Price))
                            {
                                u.AddPlayer(b);
                                u.Wallet.Gold.Pay(b.Price);
                                u.Save();
                                Broker.Global.Publish(AddPlayer.Get(b));
                                _this.successEmploy.Show().SubmitAsObservable().Subscribe(_ => PopupManager.Close());
                            }
                            else
                            {
                                _this.failedEmploy.Show().SubmitAsObservable().Subscribe(_ => PopupManager.Close());
                            }
                            break;
                        case CharacterDetailsPopupController.SubmitType.EmployCancel:
                            PopupManager.Close(_popup);
                            break;
                        default:
                            Assert.IsTrue(false, $"{type}は未対応の値です");
                            break;
                    }
                })
                .AddTo(this);
        }
    }
}
