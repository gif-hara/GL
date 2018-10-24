using System;
using GL.Battle.CharacterControllers;
using GL.Battle.Commands.Bundle;
using GL.Events.Battle;
using HK.Framework.EventSystems;
using HK.GL.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Battle.UI
{
    /// <summary>
    /// キャラクターUIを制御するクラス
    /// </summary>
    public sealed class CharacterUIController : MonoBehaviour
    {
        public enum ParameterType
        {
            Status,
            Resistance,

            Max,
        }

        [SerializeField]
        private Color playerColor;

        [SerializeField]
        private Color enemyColor;

        [SerializeField]
        private Image icon;
        public Image Icon => this.icon;

        [SerializeField]
        private Button iconButton;

        [SerializeField]
        private Button parameterButton;

        [SerializeField]
        private Image background;

        [SerializeField]
        private HitPoint hitPoint;

        [SerializeField]
        private Status status;

        [SerializeField]
        private Resistance resistance;

        private Character character;

        private ParameterType parameterType = ParameterType.Status;

        public void Setup(Character character)
        {
            this.character = character;
            Assert.IsNotNull(this.character);

            this.character.UIController = this;
            var isOpponent = this.character.CharacterType == Constants.CharacterType.Enemy;

            this.background.color = isOpponent ? this.enemyColor : this.playerColor;
            this.iconButton.enabled = false;
            this.Apply();
            this.SetActiveParameter(this.parameterType);

            Broker.Global.Receive<StartSelectTarget>()
                .Where(x => x.Targets.Find(t => t == this.character) != null)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.OnClickSelectTarget(x.Character, x.Command);
                })
                .AddTo(this);

            Broker.Global.Receive<ModifiedStatus>()
                .Where(x => x.Character == this.character)
                .SubscribeWithState(this, (_, _this) => _this.Apply())
                .AddTo(this);

            this.parameterButton.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.parameterType = (ParameterType)(((int)_this.parameterType + 1) % ((int)ParameterType.Max));
                    _this.SetActiveParameter(_this.parameterType);
                })
                .AddTo(this);

            if(isOpponent)
            {
                // 相手側の場合は子要素を逆転させる
                this.transform.ReversalChildren();
                this.status.Root.transform.ReversalChildren();
                this.resistance.Root.transform.ReversalChildren();
            }
        }

        private void Apply()
        {
            this.hitPoint.Apply(this.character);
            this.status.Apply(this.character);
            this.resistance.Apply(this.character.StatusController);
        }

        private void SetActiveParameter(ParameterType parameterType)
        {
            this.status.Root.SetActive(parameterType == ParameterType.Status);
            this.resistance.Root.SetActive(parameterType == ParameterType.Resistance);
        }

        private void OnClickSelectTarget(Character invoker, Implement command)
        {
            this.iconButton.enabled = true;
            this.iconButton.OnClickAsObservable()
                .First()
                .TakeUntil(Broker.Global.Receive<SelectedTargets>())
                .SubscribeWithState3(this, invoker, command, (_, t, i, c) =>
                {
                    t.iconButton.enabled = false;
                    Broker.Global.Publish(SelectedTargets.Get(i, c, new Character[] { t.character }));
                })
                .AddTo(this);
        }

        [Serializable]
        public class HitPoint
        {
            [SerializeField]
            private GameObject root;
            public GameObject Root => this.root;

            [SerializeField]
            private Text text;

            [SerializeField]
            private Scrollbar gauge;

            public void Apply(Character character)
            {
                this.text.text = character.StatusController.HitPoint.ToString();
                this.gauge.size = character.StatusController.HitPointRate;
            }
        }

        [Serializable]
        public class Status
        {
            [SerializeField]
            private GameObject root;
            public GameObject Root => this.root;

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

            public void Apply(Character character)
            {
                this.strength.text = character.StatusController.GetTotalParameter(Constants.StatusParameterType.Strength).ToString();
                this.strengthMagic.text = character.StatusController.GetTotalParameter(Constants.StatusParameterType.StrengthMagic).ToString();
                this.defense.text = character.StatusController.GetTotalParameter(Constants.StatusParameterType.Defense).ToString();
                this.defenseMagic.text = character.StatusController.GetTotalParameter(Constants.StatusParameterType.DefenseMagic).ToString();
                this.speed.text = character.StatusController.GetTotalParameter(Constants.StatusParameterType.Speed).ToString();
            }
        }

        [Serializable]
        public class Resistance
        {
            [SerializeField]
            private GameObject root;
            public GameObject Root => this.root;

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

            public void Apply(CharacterStatusController statusController)
            {
                const string format = "P0";
                this.poison.text = statusController.GetTotalResistance(Constants.StatusAilmentType.Poison).ToString(format);
                this.paralysis.text = statusController.GetTotalResistance(Constants.StatusAilmentType.Paralysis).ToString(format);
                this.sleep.text = statusController.GetTotalResistance(Constants.StatusAilmentType.Sleep).ToString(format);
                this.confuse.text = statusController.GetTotalResistance(Constants.StatusAilmentType.Confuse).ToString(format);
                this.berserk.text = statusController.GetTotalResistance(Constants.StatusAilmentType.Berserk).ToString(format);
                this.vitals.text = statusController.GetTotalResistance(Constants.StatusAilmentType.Vitals).ToString(format);
            }
        }
    }
}
