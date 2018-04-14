using GL.Scripts.UI.PopupControllers;
using GL.Scripts.User;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Scripts.Home.UI
{
    /// <summary>
    /// 編成パネルを制御するクラス
    /// </summary>
    public sealed class OrganizationPanelController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform playersParent;

        [SerializeField]
        private PlayerButtonController[] parties;

        [SerializeField]
        private Button[] togglePartyButtons;

        [SerializeField]
        private PlayerButtonController playerButtonPrefab;

        [SerializeField]
        private PlayerDetailsPopupController playerDetailsPopupPrefab;
        
        [SerializeField]
        private Color togglePartyActiveColor;

        [SerializeField]
        private Color togglePartyDeactiveColor;
        
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
        }

        private void SetupPlayersPanel(UserData userData)
        {
            foreach (var player in userData.Players)
            {
                var controller = Instantiate(this.playerButtonPrefab, this.playersParent, false);
                controller.SetProperty(player);
                controller.Button.OnClickAsObservable()
                    .SubscribeWithState(player, (_, p) => Debug.Log(p.Blueprint.CharacterName))
                    .AddTo(controller.OnClickObserver);
            }
        }

        private void SetupPartyPanel(UserData userData)
        {
            Assert.AreEqual(this.parties.Length, Party.PlayerMax, "パーティ最大人数と一致しません");
            var party = userData.CurrentParty;
            for (var i = 0; i < this.parties.Length; i++)
            {
                var controller = this.parties[i];
                controller.SetProperty(party.Players[i]);
                controller.Button.OnClickAsObservable()
                    .SubscribeWithState2(this, party.Players[i], (_, _this, p) =>
                    {
                        _this.CreatePlayerDetailsPopup(p);
                    })
                    .AddTo(controller.OnClickObserver);
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

        private void CreatePlayerDetailsPopup(Player player)
        {
            PopupController.Instance.Instantiate(this.playerDetailsPopupPrefab)
                .Setup(player);
        }
    }
}
