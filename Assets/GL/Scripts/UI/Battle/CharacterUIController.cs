using System;
using DG.Tweening;
using GL.Battle.CharacterControllers;
using GL.Battle.Commands.Bundle;
using GL.Events.Battle;
using GL.Home.UI;
using GL.Tweens;
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

        [SerializeField]
        private TargetableTween targetableTween;

        [SerializeField]
        private AttackTween attackTween;

        private Character character;

        private Tween currentTween;

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
                    _this.ChangeTween(_this.targetableTween.Apply(_this.icon.transform));
                    _this.ClearTweenOnSelectedTargets();
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

        public void StartAttack(Action onAttack)
        {
            this.attackTween.Apply(this.icon.transform, onAttack, this.character.CharacterType == Constants.CharacterType.Enemy);
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

        private void ChangeTween(Tween tween)
        {
            if(this.currentTween != null)
            {
                this.currentTween.Kill();
            }

            this.currentTween = tween;
        }

        private void ClearTween()
        {
            if(this.currentTween == null)
            {
                return;
            }

            this.currentTween.Kill();
            this.currentTween = null;
        }

        private void ClearTweenOnSelectedTargets()
        {
            Broker.Global.Receive<SelectedTargets>()
                .Take(1)
                .SubscribeWithState(this, (_, _this) => _this.ClearTween())
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
