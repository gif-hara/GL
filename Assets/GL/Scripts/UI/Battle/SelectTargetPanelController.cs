using UnityEngine;
using System.Collections.Generic;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using GL.Scripts.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using GL.Scripts.Battle.Commands.Implements;

namespace HK.GL.UI.Battle
{
    /// <summary>
    /// ターゲットを選択するパネルを制御する
    /// </summary>
    public sealed class SelectTargetPanelController : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private List<SelectTargetButtonController> buttons;

        void Awake()
        {
            Observable.Merge(
                Broker.Global.Receive<StartSelectCommand>().Select(x => Unit.Default),
                Broker.Global.Receive<SelectedTargets>().Select(x => Unit.Default)
            )
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.SetActive(false);
                })
                .AddTo(this);

            Broker.Global.Receive<StartSelectTarget>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    if (x.Character.CharacterType == Constants.CharacterType.Player)
                    {
                        _this.OnSelectTargetFromPlayer(x.Character, x.Command, x.Targets);
                    }
                    else
                    {
                        _this.OnSelectTargetFromEnemy();
                    }
                })
                .AddTo(this);
        }

        private void OnSelectTargetFromPlayer(Character character, IImplement command, Character[] targets)
        {
            this.SetActive(true);

            for (int i = 0; i < targets.Length; i++)
            {
                var button = this.buttons[i];
                var target = targets[i];
                button.gameObject.SetActive(true);
                button.SetProperty(character, command, target);
            }
            for (int i = targets.Length; i < this.buttons.Count; i++)
            {
                this.buttons[i].gameObject.SetActive(false);
            }
        }

        private void OnSelectTargetFromEnemy()
        {
            this.SetActive(false);
        }

        private void SetActive(bool isActive)
        {
            this.canvasGroup.alpha = isActive ? 1.0f : 0.0f;
            this.canvasGroup.interactable = isActive;
            this.canvasGroup.blocksRaycasts = isActive;
        }
    }
}
