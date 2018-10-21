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
            EmployDecide,
            EmployCancel,
        }

        public enum Mode
        {
            Default,
            Change,
            Employ,
        }

        [Serializable]
        public class DefaultModeElement
        {
            [SerializeField]
            private GameObject root;
            public GameObject Root => this.root;

            [SerializeField]
            private Button trainingButton;

            [SerializeField]
            private Button changeButton;

            [SerializeField]
            private Button closeButton;

            public void Apply(CharacterDetailsPopupController controller)
            {
                controller.OnClickSubmit(this.trainingButton, SubmitType.Training);
                controller.OnClickSubmit(this.changeButton, SubmitType.StartChange);
                controller.OnClickSubmit(this.closeButton, SubmitType.Close);
            }
        }

        [Serializable]
        public class ChangeModeElement
        {
            [SerializeField]
            private GameObject root;
            public GameObject Root => this.root;

            [SerializeField]
            private Button decideButton;

            [SerializeField]
            private Button cancelButton;

            public void Apply(CharacterDetailsPopupController controller)
            {
                controller.OnClickSubmit(this.decideButton, SubmitType.ChangeDecide);
                controller.OnClickSubmit(this.cancelButton, SubmitType.ChangeCancel);
            }
        }

        [Serializable]
        public class EmployModeElement
        {
            [SerializeField]
            private GameObject root;
            public GameObject Root => this.root;

            [SerializeField]
            private Button decideButton;

            [SerializeField]
            private Button cancelButton;

            [SerializeField]
            private Text decideText;

            [SerializeField]
            private StringAsset.Finder decideFormat;

            public void Apply(CharacterDetailsPopupController controller, Battle.CharacterControllers.Blueprint blueprint)
            {
                controller.OnClickSubmit(this.decideButton, SubmitType.EmployDecide);
                controller.OnClickSubmit(this.cancelButton, SubmitType.EmployCancel);
                this.decideText.text = this.decideFormat.Format(blueprint.Price.ToString());
            }
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

        [SerializeField]
        private EmployModeElement employModeElement;

        [SerializeField]
        private GameObject equipmentsRoot;

        [SerializeField]
        private GameObject commandsRoot;

        private Player editPlayer;

        /// <summary>
        /// <paramref name="player"/>をもとに表示するための設定を行う
        /// </summary>
        public CharacterDetailsPopupController Setup(Player player, Mode mode)
        {
            this.profile.Apply(player);
            this.parameter.Apply(player.Parameter);
            this.resistance.Apply(player.Resistance);
            this.rightWeapon.Setup(this, player.RightBattleWeapon);
            this.leftWeapon.Setup(this, player.LeftBattleWeapon);
            this.commandListController.Setup(player.UsingCommands);
            this.editPlayer = player;

            this.equipmentsRoot.SetActive(true);
            this.commandsRoot.SetActive(true);

            this.SetActiveModeElement(mode);

            switch(mode)
            {
                case Mode.Default:
                    this.defaultModeElement.Apply(this);
                    break;
                case Mode.Change:
                    this.changeModeElement.Apply(this);
                    break;
                default:
                    Assert.IsTrue(false, $"{mode}は未対応の値です");
                    break;
            }


            return this;
        }

        public CharacterDetailsPopupController Setup(Battle.CharacterControllers.Blueprint blueprint, Mode mode)
        {
            this.profile.Apply(blueprint);
            this.parameter.Apply(blueprint.Min);
            this.resistance.Apply(blueprint.Resistance);

            this.equipmentsRoot.SetActive(false);
            this.commandsRoot.SetActive(false);

            this.SetActiveModeElement(mode);

            switch(mode)
            {
                case Mode.Employ:
                    this.employModeElement.Apply(this, blueprint);
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

        private void SetActiveModeElement(Mode mode)
        {
            this.defaultModeElement.Root.SetActive(mode == Mode.Default);
            this.changeModeElement.Root.SetActive(mode == Mode.Change);
            this.employModeElement.Root.SetActive(mode == Mode.Employ);
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

            public void Apply(Battle.CharacterControllers.Blueprint blueprint)
            {
                this.characterName.text = blueprint.CharacterName;
                this.jobName.text = blueprint.Job.JobName;
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
            private Text strengthMagic;

            [SerializeField]
            private Text defense;

            [SerializeField]
            private Text defenseMagic;

            [SerializeField]
            private Text speed;

            public void Apply(Battle.CharacterControllers.Parameter parameter)
            {
                this.hitPoint.text = parameter.HitPoint.ToString();
                this.strength.text = parameter.Strength.ToString();
                this.strengthMagic.text = parameter.StrengthMagic.ToString();
                this.defense.text = parameter.Defense.ToString();
                this.defenseMagic.text = parameter.DefenseMagic.ToString();
                this.speed.text = parameter.Speed.ToString();
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
    }
}
