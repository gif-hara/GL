using UnityEngine;
using System.Collections.Generic;
using GL.Battle.CharacterControllers;
using GL.Battle;
using GL.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using GL;
using UnityEngine.UI;

namespace GL.Battle.UI
{
    /// <summary>
    /// コマンドパネルを制御するクラス
    /// </summary>
    public sealed class SelectCommandPanelController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform buttonParent;

        [SerializeField]
        private SelectCommandButtonController buttonPrefab;

        [SerializeField]
        private float heightMax;

        [SerializeField]
        private LayoutGroup rootLayoutGroup;

        [SerializeField]
        private GridLayoutGroup commandLayoutGroup;

        private RectTransform cachedTransform;

        void Awake()
        {
            this.cachedTransform = (RectTransform)this.transform;

            Broker.Global.Receive<VisibleRequestSelectCommandPanel>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    if (x.Character.CharacterType == Constants.CharacterType.Player)
                    {
                        _this.OnSelectCommandFromPlayer(x.Character);
                    }
                    else
                    {
                        _this.OnSelectCommandFromEnemy();
                    }
                })
                .AddTo(this);

            Broker.Global.Receive<SelectedCommand>()
                .SubscribeWithState(this, (_, _this) => _this.gameObject.SetActive(false))
                .AddTo(this);
        }

        private void OnSelectCommandFromPlayer(Character character)
        {
            this.gameObject.SetActive(true);
            this.DestroyButtons();
            if (!character.CanMove)
            {
                return;
            }
            
            var commands = character.StatusController.Commands;
            for (int i = 0; i < commands.Length; i++)
            {
                var button = Instantiate(this.buttonPrefab, this.buttonParent);
                var command = commands[i];
                button.SetProperty(character, command);
            }

            var r = this.rootLayoutGroup;
            var c = this.commandLayoutGroup;
            var constraintCount = ((commands.Length - 1) / c.constraintCount) + 1;
            var height = (c.cellSize.y * constraintCount) + (c.spacing.y * (constraintCount - 1)) + c.padding.bottom + c.padding.top + r.padding.bottom + r.padding.top;
            var sizeDelta = this.cachedTransform.sizeDelta;
            sizeDelta.y = Mathf.Min(height, this.heightMax);
            this.cachedTransform.sizeDelta = sizeDelta;

            var buttonParentPosition = this.buttonParent.anchoredPosition;
            buttonParentPosition.y = 0;
            this.buttonParent.anchoredPosition = buttonParentPosition;
        }

        private void OnSelectCommandFromEnemy()
        {
            this.gameObject.SetActive(false);
            this.DestroyButtons();
        }

        private void DestroyButtons()
        {
            // TODO: ObjectPool使う？
            for (var i = 0; i < this.buttonParent.childCount; i++)
            {
                Destroy(this.buttonParent.GetChild(i).gameObject);
            }
        }
    }
}
