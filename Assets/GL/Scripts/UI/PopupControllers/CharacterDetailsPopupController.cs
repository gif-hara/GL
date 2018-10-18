using System;
using GL.User;
using HK.Framework.Text;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GL.UI.PopupControllers
{
    /// <summary>
    /// キャラクター詳細ポップアップを制御するクラス
    /// </summary>
    public sealed class CharacterDetailsPopupController : PopupBase
    {
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

        private Player editPlayer;

        public void Setup(Player player)
        {
            this.profile.Apply(player);
            this.parameter.Apply(player.Parameter);
            this.resistance.Apply(player.Resistance);
            this.rightWeapon.Setup(player.RightBattleWeapon);
            this.leftWeapon.Setup(player.LeftBattleWeapon);
            this.commandListController.Setup(player.UsingCommands);
            this.editPlayer = player;
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
                        if(_handType == Constants.HandType.Right)
                        {
                            _this.rightWeapon.Setup(_this.editPlayer.RightBattleWeapon);
                        }
                        else
                        {
                            _this.leftWeapon.Setup(_this.editPlayer.LeftBattleWeapon);
                        }
                        _this.commandListController.Setup(_this.editPlayer.UsingCommands);
                    }

                    PopupManager.Close(_popup);
                });
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
