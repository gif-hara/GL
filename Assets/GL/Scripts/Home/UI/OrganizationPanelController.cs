using System.Linq;
using GL.Events.Home;
using GL.UI.PopupControllers;
using GL.User;
using HK.Framework.EventSystems;
using HK.GL.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// 編成パネルを制御するクラス
    /// </summary>
    public sealed class OrganizationPanelController : MonoBehaviour
    {
        /// <summary>
        /// 編成モード
        /// </summary>
        public enum OrganizationMode
        {
            /// <summary>
            /// 通常モード
            /// </summary>
            Default,

            /// <summary>
            /// 交換モード
            /// </summary>
            Change,
        }

        [SerializeField]
        private RectTransform playersParent;

        [SerializeField]
        private PlayerButtonController[] parties;

        [SerializeField]
        private Button[] togglePartyButtons;

        [SerializeField]
        private PlayerButtonController playerButtonPrefab;

        [SerializeField]
        private CharacterDetailsPopupController characterDetailsPopupPrefab;
        
        [SerializeField]
        private Color togglePartyActiveColor;

        [SerializeField]
        private Color togglePartyDeactiveColor;

        [SerializeField]
        private TrainingPopupController trainingPopupController;

        [SerializeField]
        private SimplePopupStrings failedLevelUp;

        private OrganizationMode currentMode;

        /// <summary>
        /// 交代中のプレイヤー
        /// </summary>
        private Player changeTargetPlayer;

        void Start()
        {
            var userData = UserData.Instance;
            
            userData.CurrentPartyIndex
                .SubscribeWithState2(this, userData, (_, _this, u) =>
                {
                    _this.SetupPartyPanel(u);
                })
                .AddTo(this);
        
            this.SetupPlayersPanel(userData);
            this.SetupTogglePartyPanel(userData);

            Broker.Global.Receive<AddPlayer>()
                .SubscribeWithState(this, (_, _this) => _this.SetupPlayersPanel(UserData.Instance))
                .AddTo(this);
        }

        private void SetupPlayersPanel(UserData userData)
        {
            for (var i = 0; i < this.playersParent.childCount; i++)
            {
                Destroy(this.playersParent.GetChild(i).gameObject);
            }

            foreach (var player in userData.Players.List.Where(p => !userData.CurrentParty.Contains(p)))
            {
                var controller = Instantiate(this.playerButtonPrefab, this.playersParent, false);
                this.ApplyPlayerButtonController(controller, player);
            }
        }

        private void SetupPartyPanel(UserData userData)
        {
            Assert.AreEqual(this.parties.Length, Party.PlayerMax, "パーティ最大人数と一致しません");
            var party = userData.CurrentParty;
            for (var i = 0; i < this.parties.Length; i++)
            {
                var controller = this.parties[i];
                var player = userData.Players.GetByInstanceId(party.PlayerInstanceIds[i]);
                this.ApplyPlayerButtonController(controller, player);
            }
        }

        private void SetupTogglePartyPanel(UserData userData)
        {
            Assert.AreEqual(this.togglePartyButtons.Length, UserData.PartyCount, "最大パーティ数と一致しません");
            userData.CurrentPartyIndex
                .SubscribeWithState(this, (index, _this) =>
                {
                    for (var i = 0; i < _this.togglePartyButtons.Length; i++)
                    {
                        _this.togglePartyButtons[i].targetGraphic.color = i == index
                            ? _this.togglePartyActiveColor
                            : _this.togglePartyDeactiveColor;
                    }
                })
                .AddTo(this);
            for (var i = 0; i < this.togglePartyButtons.Length; i++)
            {
                this.togglePartyButtons[i].OnClickAsObservable()
                    .SubscribeWithState2(userData, i, (_, u, index) =>
                    {
                        u.SetCurrentPartyIndex(index);
                    })
                    .AddTo(this);
            }
        }

        private void ApplyPlayerButtonController(PlayerButtonController controller, Player player)
        {
            controller.Setup(player);
            controller.Button.OnClickAsObservable()
                .Where(_ => this.CanShowPlayerDitailsPopup(player))
                .SubscribeWithState2(this, player, (_, _this, p) =>
                {
                    _this.ShowPlayerDetailsPopup(p);
                })
                .AddTo(controller.Disposable);
        }

        private bool CanShowPlayerDitailsPopup(Player player)
        {
            if(this.currentMode == OrganizationMode.Default)
            {
                return true;
            }

            return this.currentMode == OrganizationMode.Change && this.changeTargetPlayer != player;
        }

        private void ShowPlayerDetailsPopup(Player player)
        {
            var popup = PopupManager.Show(this.characterDetailsPopupPrefab);
            popup
                .Setup(player, this.ConvertToMode)
                .SubmitAsObservable()
                .Select(x => (CharacterDetailsPopupController.SubmitType)x)
                .SubscribeWithState3(this, popup, player, (x, _this, _popup, _player) =>
                {
                    switch(x)
                    {
                        case CharacterDetailsPopupController.SubmitType.Close:
                            _this.OnClose(_popup);
                            break;
                        case CharacterDetailsPopupController.SubmitType.StartChange:
                            _this.OnStartChange(_popup, _player);
                            break;
                        case CharacterDetailsPopupController.SubmitType.Training:
                            _this.OnTraining(_popup, _player);
                            break;
                        case CharacterDetailsPopupController.SubmitType.ChangeDecide:
                            _this.OnChangeDecide(_popup, _player);
                            break;
                        case CharacterDetailsPopupController.SubmitType.ChangeCancel:
                            _this.OnChangeCancel(_popup);
                            break;
                        default:
                            Assert.IsTrue(false, $"{x}は未対応の値です");
                            break;
                    }
                })
                .AddTo(this);
        }

        private void OnClose(CharacterDetailsPopupController popup)
        {
            PopupManager.Close(popup);
        }

        private void OnStartChange(CharacterDetailsPopupController popup, Player player)
        {
            PopupManager.Close(popup);
            this.currentMode = OrganizationMode.Change;
            this.changeTargetPlayer = player;
            this.SetTogglePartyInteractable(false);
            Broker.Global.Publish(StartPlayerChange.Get(player));
            Broker.Global.Publish(ChangeFooter.Get(FooterController.Mode.Cancel));
            Broker.Global.Receive<ClickedFooterCancelButton>()
                .Take(1)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.currentMode = OrganizationMode.Default;
                    _this.changeTargetPlayer = null;
                    _this.SetTogglePartyInteractable(true);
                    Broker.Global.Publish(ChangeFooter.Get(FooterController.Mode.Default));
                    Broker.Global.Publish(CompletePlayerChange.Get(null));
                })
                .AddTo(this);
        }

        private void OnTraining(CharacterDetailsPopupController popup, Player player)
        {
            var trainingPopup = PopupManager.Show(this.trainingPopupController).Setup(player);
            trainingPopup.SubmitAsObservable()
                .SubscribeWithState2(this, trainingPopup, (x, _this, _trainingPopup) =>
                {
                    switch((TrainingPopupController.SubmitType)x)
                    {
                        case TrainingPopupController.SubmitType.LevelUp:
                            this.LevelUp(player);
                            break;
                        case TrainingPopupController.SubmitType.Cancel:
                            PopupManager.Close(_trainingPopup);
                            break;
                    }
                })
                .AddTo(this);
        }

        private void OnChangeDecide(CharacterDetailsPopupController popup, Player player)
        {
            PopupManager.Close(popup);
            Assert.IsNotNull(this.changeTargetPlayer);
            UserData.Instance.CurrentParty.Change(this.changeTargetPlayer.InstanceId, player.InstanceId);
            this.SetupPartyPanel(UserData.Instance);
            this.SetupPlayersPanel(UserData.Instance);
            this.currentMode = OrganizationMode.Default;
            this.changeTargetPlayer = null;
            this.SetTogglePartyInteractable(true);
            Broker.Global.Publish(ChangeFooter.Get(FooterController.Mode.Default));
            Broker.Global.Publish(CompletePlayerChange.Get(player));
            UserData.Instance.Save();
        }

        private void OnChangeCancel(CharacterDetailsPopupController popup)
        {
            PopupManager.Close(popup);
        }

        private void LevelUp(Player player)
        {
            Assert.AreNotEqual(player.Level, Constants.LevelMax);
            var u = UserData.Instance;
            var needExperience = player.CharacterRecord.Experience.GetNeedValue(player.Level + 1);
            if(u.Wallet.Experience.IsEnough(needExperience))
            {
                player.LevelUp();
                u.Wallet.Experience.Pay(needExperience);
                u.Save();
                Broker.Global.Publish(LevelUppedPlayer.Get(player));
            }
            else
            {
                this.failedLevelUp.Show().SubmitAsObservable().Subscribe(_ => PopupManager.Close());
            }
        }

        private void SetTogglePartyInteractable(bool interactable)
        {
            this.togglePartyButtons.ForEach(b => b.interactable = interactable);
        }

        private CharacterDetailsPopupController.Mode ConvertToMode
        {
            get
            {
                switch(this.currentMode)
                {
                    case OrganizationMode.Default:
                        return CharacterDetailsPopupController.Mode.Default;
                    case OrganizationMode.Change:
                        return CharacterDetailsPopupController.Mode.Change;
                    default:
                        Assert.IsTrue(false, $"{this.currentMode}は未対応の値です");
                        return CharacterDetailsPopupController.Mode.Default;
                }
            }
        }
    }
}
