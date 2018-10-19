using System;
using GL.UI;
using GL.UI.PopupControllers;
using GL.User;
using HK.Framework.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// キャラクター詳細ポップアップを制御するクラス
    /// </summary>
    public sealed class CharacterDetailsPopupController : PopupBase
    {
        public enum SubmitType
        {
            Close,
            Training,
            StartChange,
            ChangeDecide,
            ChangeCancel,
        }

        public enum Mode
        {
            Default,
            Change,
        }

        [Serializable]
        public class DefaultModeElement
        {
            [SerializeField]
            private GameObject root;
            public GameObject Root => this.root;

            [SerializeField]
            private Button trainingButton;
            public Button TrainingButton => this.trainingButton;

            [SerializeField]
            private Button changeButton;
            public Button ChangeButton => this.changeButton;

            [SerializeField]
            private Button closeButton;
            public Button CloseButton => this.closeButton;
        }

        [Serializable]
        public class ChangeModeElement
        {
            [SerializeField]
            private GameObject root;
            public GameObject Root => this.root;

            [SerializeField]
            private Button decideButton;
            public Button DecideButton => this.decideButton;

            [SerializeField]
            private Button cancelButton;
            public Button CancelButton => this.cancelButton;
        }

        [SerializeField]
        private Profile profile;
        
        [SerializeField]
        private Parameter parameter;

        [SerializeField]
        private Resistance resistance;

        [SerializeField]
        private CharacterDetailsPopupWeaponController rightWeapon;

        [SerializeField]
        private CharacterDetailsPopupWeaponController leftWeapon;

        [SerializeField]
        private CommandListController commandListController;

        [SerializeField]
        private EquippedWeaponPopupController equippedWeaponPopupController;

        [SerializeField]
        private DefaultModeElement defaultModeElement;

        [SerializeField]
        private ChangeModeElement changeModeElement;

        private Player editPlayer;

        public CharacterDetailsPopupController Setup(Player player, Mode mode)
        {
            this.profile.Apply(player);
            this.parameter.Apply(player.Parameter);
            this.resistance.Apply(player.Resistance);
            this.rightWeapon.Setup(this, player.RightBattleWeapon);
            this.leftWeapon.Setup(this, player.LeftBattleWeapon);
            this.commandListController.Setup(player.UsingCommands);
            this.editPlayer = player;

            this.defaultModeElement.Root.SetActive(mode == Mode.Default);
            this.changeModeElement.Root.SetActive(mode == Mode.Change);

            switch(mode)
            {
                case Mode.Default:
                    this.OnClickSubmit(this.defaultModeElement.TrainingButton, SubmitType.Training);
                    this.OnClickSubmit(this.defaultModeElement.ChangeButton, SubmitType.StartChange);
                    this.OnClickSubmit(this.defaultModeElement.CloseButton, SubmitType.Close);
                    break;
                case Mode.Change:
                    this.OnClickSubmit(this.changeModeElement.DecideButton, SubmitType.ChangeDecide);
                    this.OnClickSubmit(this.changeModeElement.CancelButton, SubmitType.ChangeCancel);
                    break;
                default:
                    Assert.IsTrue(false, $"{mode}は未対応の値です");
                    break;
            }


            return this;
        }

        public void ShowEquippedWeaponPopup(Constants.HandType handType)
        {
            var popup = PopupManager.Show(this.equippedWeaponPopupController);
            popup
                .Setup(this.editPlayer, handType)
                .SubmitAsObservable()
                .SubscribeWithState3(this, popup, handType, (instanceId, _this, _popup, _handType) =>
                {
                    if(instanceId != -1)
                    {
                        _this.editPlayer.ChangeWeapon(_handType, instanceId);
                        UserData.Instance.Save();
                        if(_handType == Constants.HandType.Right)
                        {
                            _this.rightWeapon.Setup(_this, _this.editPlayer.RightBattleWeapon);
                        }
                        else
                        {
                            _this.leftWeapon.Setup(_this, _this.editPlayer.LeftBattleWeapon);
                        }
                        _this.commandListController.Setup(_this.editPlayer.UsingCommands);
                    }

                    PopupManager.Close(_popup);
                });
        }

        private void OnClickSubmit(Button button, SubmitType type)
        {
            button.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) => _this.submit.OnNext((int)type))
                .AddTo(this);
        }

        [Serializable]
        private class Profile
        {
            [SerializeField]
            private Text characterName;

            [SerializeField]
            private Text jobName;

            public void Apply(Player player)
            {
                this.characterName.text = player.PlayerName;
                this.jobName.text = player.Blueprint.Job.JobName;
            }
        }

        [Serializable]
        public class Parameter
        {
            [SerializeField]
            private Text hitPoint;

            [SerializeField]
            private Text strength;

            [SerializeField]
            private Text defense;

            [SerializeField]
            private Text speed;

            [SerializeField]
            private Text luck;

            public void Apply(Battle.CharacterControllers.Parameter parameter)
            {
                this.hitPoint.text = parameter.HitPoint.ToString();
                this.strength.text = parameter.Strength.ToString();
                this.defense.text = parameter.Defense.ToString();
                this.speed.text = parameter.Speed.ToString();
                this.luck.text = parameter.Luck.ToString();
            }
        }

        [Serializable]
        public class Resistance
        {
            [SerializeField]
            private Text poison;

            [SerializeField]
            private Text paralysis;

            [SerializeField]
            private Text sleep;

            [SerializeField]
            private Text confuse;

            [SerializeField]
            private Text berserk;

            [SerializeField]
            private Text vitals;

            public void Apply(Battle.CharacterControllers.Resistance resistance)
            {
                const string format = "P0";
                this.poison.text = resistance.Poison.ToString(format);
                this.paralysis.text = resistance.Paralysis.ToString(format);
                this.sleep.text = resistance.Sleep.ToString(format);
                this.confuse.text = resistance.Confuse.ToString(format);
                this.berserk.text = resistance.Berserk.ToString(format);
                this.vitals.text = resistance.Vitals.ToString(format);
            }
        }

        [Serializable]
        public class ButtonElement
        {
            [SerializeField]
            private Button button;
            public Button Button { get { return button; } }

            [SerializeField]
            private Text text;
            public Text Text { get { return text; } }
        }
    }
}
