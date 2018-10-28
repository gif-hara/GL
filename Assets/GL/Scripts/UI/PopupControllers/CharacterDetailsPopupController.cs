﻿using System;
using GL.Battle.CharacterControllers;
using GL.Database;
using GL.Events.Home;
using GL.UI;
using GL.UI.PopupControllers;
using GL.User;
using HK.Framework.EventSystems;
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
            BattleClose,
        }

        public enum Mode
        {
            Default,
            Change,
            Employ,
            Battle,
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

            public void Apply(CharacterDetailsPopupController controller, CharacterRecord blueprint)
            {
                controller.OnClickSubmit(this.decideButton, SubmitType.EmployDecide);
                controller.OnClickSubmit(this.cancelButton, SubmitType.EmployCancel);
                this.decideText.text = this.decideFormat.Format(blueprint.Price.ToString());
            }
        }

        [Serializable]
        public class BattleModeElement
        {
            [SerializeField]
            private GameObject root;
            public GameObject Root => this.root;

            [SerializeField]
            private Button closeButton;

            public void Apply(CharacterDetailsPopupController controller)
            {
                controller.OnClickSubmit(this.closeButton, SubmitType.BattleClose);
            }
        }

        [SerializeField]
        private Rank rank;

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
        private BattleModeElement battleModeElement;

        [SerializeField]
        private GameObject equipmentsRoot;

        [SerializeField]
        private GameObject commandsRoot;

        /// <summary>
        /// 同じ武器を装備しようとした時のポップアップ
        /// </summary>
        [SerializeField]
        private SimplePopupStrings failedChangeWeaponFromSame;

        private Player editPlayer;

        /// <summary>
        /// <paramref name="player"/>をもとに表示するための設定を行う
        /// </summary>
        public CharacterDetailsPopupController Setup(Player player, Mode mode)
        {
            this.rank.Apply(player.CharacterRecord);
            this.profile.Apply(player);
            this.parameter.Apply(player.Parameter);
            this.resistance.Apply(player.Resistance);
            this.rightWeapon.Setup(this, player.RightHand.BattleWeapon);
            this.leftWeapon.Setup(this, player.LeftHand.BattleWeapon);
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

            Broker.Global.Receive<LevelUppedPlayer>()
                .Where(x => x.Player == player)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.profile.Apply(x.Player);
                    _this.parameter.Apply(x.Player.Parameter);
                })
                .AddTo(this);


            return this;
        }

        public CharacterDetailsPopupController Setup(CharacterRecord characterRecord, Mode mode)
        {
            this.rank.Apply(characterRecord);
            this.profile.Apply(characterRecord);
            this.parameter.Apply(characterRecord.Min);
            this.resistance.Apply(characterRecord.Resistance);

            this.equipmentsRoot.SetActive(false);
            this.commandsRoot.SetActive(false);

            this.SetActiveModeElement(mode);

            switch(mode)
            {
                case Mode.Employ:
                    this.employModeElement.Apply(this, characterRecord);
                    break;
                default:
                    Assert.IsTrue(false, $"{mode}は未対応の値です");
                    break;
            }

            return this;
        }

        public CharacterDetailsPopupController Setup(Character character)
        {
            this.rank.Apply(character.StatusController.CharacterRecord);
            this.profile.Apply(character);
            this.parameter.Apply(character);
            this.resistance.Apply(character);

            this.equipmentsRoot.SetActive(false);
            this.commandsRoot.SetActive(false);
            this.SetActiveModeElement(Mode.Battle);

            this.battleModeElement.Apply(this);

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
                    // 戻るボタンが押されたら何もしない
                    if(instanceId == -1)
                    {
                        PopupManager.Close(_popup);
                        return;
                    }

                    _this.editPlayer.ChangeWeapon(_handType, instanceId);
                    UserData.Instance.Save();
                    if(_handType == Constants.HandType.Right)
                    {
                        _this.rightWeapon.Setup(_this, _this.editPlayer.RightHand.BattleWeapon);
                    }
                    else
                    {
                        _this.leftWeapon.Setup(_this, _this.editPlayer.LeftHand.BattleWeapon);
                    }
                    _this.commandListController.Setup(_this.editPlayer.UsingCommands);
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
            this.battleModeElement.Root.SetActive(mode == Mode.Battle);
        }

        [Serializable]
        private class Rank
        {
            [SerializeField]
            private Transform parent;

            [SerializeField]
            private GameObject rankImage;

            public void Apply(CharacterRecord characterRecord)
            {
                this.Instantiate(characterRecord.Rank);
            }

            private void Instantiate(int count)
            {
                for (var i = 0; i < count; i++)
                {
                    UnityEngine.Object.Instantiate(this.rankImage, this.parent, false);
                }
            }
        }

        [Serializable]
        private class Profile
        {
            [SerializeField]
            private Text level;

            [SerializeField]
            private StringAsset.Finder levelFormat;

            [SerializeField]
            private Text characterName;

            [SerializeField]
            private Text jobName;

            public void Apply(Player player)
            {
                this.level.text = this.levelFormat.Format(player.Level.ToString());
                this.characterName.text = player.CharacterRecord.CharacterName;
                this.jobName.text = player.CharacterRecord.Job.JobName;
            }

            public void Apply(CharacterRecord blueprint)
            {
                this.level.text = this.levelFormat.Format(1.ToString());
                this.characterName.text = blueprint.CharacterName;
                this.jobName.text = blueprint.Job.JobName;
            }

            public void Apply(Character character)
            {
                var s = character.StatusController;
                this.level.text = this.levelFormat.Format(s.Level.ToString());
                this.characterName.text = s.CharacterRecord.CharacterName;
                this.jobName.text = s.CharacterRecord.Job.JobName;
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

            public void Apply(Character character)
            {
                var s = character.StatusController;
                this.hitPoint.text = s.HitPoint.ToString();
                this.strength.text = s.GetTotalParameter(Constants.StatusParameterType.Strength).ToString();
                this.strengthMagic.text = s.GetTotalParameter(Constants.StatusParameterType.StrengthMagic).ToString();
                this.defense.text = s.GetTotalParameter(Constants.StatusParameterType.Defense).ToString();
                this.defenseMagic.text = s.GetTotalParameter(Constants.StatusParameterType.DefenseMagic).ToString();
                this.speed.text = s.GetTotalParameter(Constants.StatusParameterType.Speed).ToString();
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

            public void Apply(Character character)
            {
                const string format = "P0";
                var s = character.StatusController;
                this.poison.text = s.GetTotalResistance(Constants.StatusAilmentType.Poison).ToString(format);
                this.paralysis.text = s.GetTotalResistance(Constants.StatusAilmentType.Paralysis).ToString(format);
                this.sleep.text = s.GetTotalResistance(Constants.StatusAilmentType.Sleep).ToString(format);
                this.confuse.text = s.GetTotalResistance(Constants.StatusAilmentType.Confuse).ToString(format);
                this.berserk.text = s.GetTotalResistance(Constants.StatusAilmentType.Berserk).ToString(format);
                this.vitals.text = s.GetTotalResistance(Constants.StatusAilmentType.Vitals).ToString(format);
            }
        }
    }
}
