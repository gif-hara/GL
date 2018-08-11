using UnityEngine;
using System.Collections.Generic;
using GL.Battle.CharacterControllers;
using GL.Battle;
using GL.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using GL.Battle.Commands.Bundle;

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
        private SelectTargetButtonController buttonPrefab;

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

        private void OnSelectTargetFromPlayer(Character character, Implement command, Character[] targets)
        {
            this.SetActive(true);
            this.DestroyButtons();

            for (int i = 0; i < targets.Length; i++)
            {
                var button = Instantiate(this.buttonPrefab, this.transform);
                var target = targets[i];
                button.SetProperty(character, command, target);
            }
        }

        private void OnSelectTargetFromEnemy()
        {
            this.SetActive(false);
            this.DestroyButtons();
        }

        private void SetActive(bool isActive)
        {
            this.canvasGroup.alpha = isActive ? 1.0f : 0.0f;
            this.canvasGroup.interactable = isActive;
            this.canvasGroup.blocksRaycasts = isActive;
        }

        private void DestroyButtons()
        {
            // TODO: ObjectPool使う？
            for (var i = 0; i < this.transform.childCount; i++)
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }
    }
}
