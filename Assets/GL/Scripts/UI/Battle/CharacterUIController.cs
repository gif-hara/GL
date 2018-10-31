using System;
using GL.Battle.CharacterControllers;
using GL.Battle.Commands.Bundle;
using GL.Events.Battle;
using GL.Home.UI;
using GL.Test;
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
        private CharacterUIAnimation animationController;
        public CharacterUIAnimation AnimationController => this.animationController;

        [SerializeField]
        private CharacterDetailsPopupController characterDetailsPopupController;

        private Character character;

        void Awake()
        {
            this.character = this.GetComponent<Character>();
            Assert.IsNotNull(this.character);

            Broker.Global.Receive<StartSelectCommand>()
                .Where(x => x.Character == this.character)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.animationController.StartEmphasisScaleAnimation();
                })
                .AddTo(this);

            Broker.Global.Receive<StartSelectTarget>()
                .Where(x => x.Targets.Find(t => t == this.character) != null)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.OnClickSelectTarget(x.Character, x.Command);
                    _this.animationController.StartTargetableAnimation();
                    _this.ClearTweenOnSelectedTargets();
                })
                .AddTo(this);

            Broker.Global.Receive<ModifiedStatus>()
                .Where(x => x.Character == this.character)
                .SubscribeWithState(this, (_, _this) => _this.Apply())
                .AddTo(this);

            Broker.Global.Receive<DamageNotify>()
                .Where(x => x.Receiver == this.character)
                .SubscribeWithState(this, (_, _this) => _this.animationController.StartDamageAnimation())
                .AddTo(this);

            this.iconLongPointerDownTrigger.OnLongPointerDownAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    PopupManager.Show(_this.characterDetailsPopupController).Setup(_this.character).CloseOnSubmit();
                })
                .AddTo(this);
        }

        void Start()
        {
            var isOpponent = this.character.CharacterType == Constants.CharacterType.Enemy;

            this.character.Record.ApplyIcon(this.icon);
            this.background.color = isOpponent ? this.enemyColor : this.playerColor;
            this.iconButton.enabled = false;
            this.Apply();

            if(isOpponent)
            {
                // 相手側の場合は子要素を逆転させる
                this.transform.ReversalChildren();
            }
        }

        public void StartAttack(Action onAttack)
        {
            this.animationController.StartAttackAnimation(onAttack, this.character.CharacterType == Constants.CharacterType.Enemy);
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
                    Broker.Global.Publish(SelectedTargets.Get(i, c, BattleManager.Instance.Parties.GetTargets(t.character, c.TargetType)));
                })
                .AddTo(this);
        }

        private void ClearTweenOnSelectedTargets()
        {
            Broker.Global.Receive<SelectedTargets>()
                .Take(1)
                .SubscribeWithState(this, (_, _this) => _this.animationController.ClearTween())
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
