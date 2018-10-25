using System;
using GL.Battle.CharacterControllers;
using GL.Battle.Commands.Bundle;
using GL.Events.Battle;
using GL.Home.UI;
using GL.UI;
using GL.UI.PopupControllers;
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
        private ObservableLongPointerDownTrigger iconLongPointerDownTrigger;

        [SerializeField]
        private Image background;

        [SerializeField]
        private HitPoint hitPoint;

        [SerializeField]
        private CharacterDetailsPopupController characterDetailsPopupController;

        private Character character;

        private ParameterType parameterType = ParameterType.Status;

        void Start()
        {
            this.character = this.GetComponent<Character>();
            Assert.IsNotNull(this.character);

            var isOpponent = this.character.CharacterType == Constants.CharacterType.Enemy;

            this.character.Record.ApplyIcon(this.icon);
            this.background.color = isOpponent ? this.enemyColor : this.playerColor;
            this.iconButton.enabled = false;
            this.Apply();

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

            this.iconLongPointerDownTrigger.OnLongPointerDownAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    PopupManager.Show(_this.characterDetailsPopupController).Setup(_this.character).CloseOnSubmit();
                })
                .AddTo(this);

            if(isOpponent)
            {
                // 相手側の場合は子要素を逆転させる
                this.transform.ReversalChildren();
            }
        }

        private void Apply()
        {
            this.hitPoint.Apply(this.character);
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
    }
}
