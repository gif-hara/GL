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
        [SerializeField]
        private Color playerColor;

        [SerializeField]
        private Color enemyColor;

        [SerializeField]
        private Image icon;

        [SerializeField]
        private Button iconButton;

        [SerializeField]
        private Image background;

        [SerializeField]
        private HitPoint hitPoint;

        [SerializeField]
        private Status status;

        private Character character;

        public void Setup(Character character)
        {
            this.character = character;
            Assert.IsNotNull(this.character);

            var isOpponent = this.character.CharacterType == Constants.CharacterType.Enemy;

            this.background.color = isOpponent ? this.enemyColor : this.playerColor;
            this.iconButton.enabled = false;
            this.Apply();

            Broker.Global.Receive<StartSelectTarget>()
                .Where(x => x.Targets.Find(t => t == this.character) != null)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.OnClickSelectTarget(x.Character, x.Command);
                });

            Broker.Global.Receive<ModifiedStatus>()
                .Where(x => x.Character == this.character)
                .SubscribeWithState(this, (_, _this) => _this.Apply());

            if(isOpponent)
            {
                // 相手側の場合は子要素を逆転させる
                this.transform.ReversalChildren();
                this.status.Root.transform.ReversalChildren();
            }
        }

        private void Apply()
        {
            this.hitPoint.Apply(this.character);
            this.status.Apply(this.character);
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
    }
}
