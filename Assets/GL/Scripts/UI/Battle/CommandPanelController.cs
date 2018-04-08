using UnityEngine;
using System.Collections.Generic;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using GL.Scripts.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;

namespace HK.GL.UI.Battle
{
    /// <summary>
    /// コマンドパネルを制御するヤーツ
    /// </summary>
    public sealed class CommandPanelController : MonoBehaviour
    {
        [SerializeField]
        private List<CommandButtonController> buttons;

        void Awake()
        {
            Broker.Global.Receive<SelectCommand>()
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

            Broker.Global.Receive<InvokeCommand>()
                .SubscribeWithState(this, (_, _this) => _this.SetDeactiveAllButton())
                .AddTo(this);
        }

        private void OnSelectCommandFromPlayer(Character character)
        {
            if (!character.CanMove)
            {
                this.SetDeactiveAllButton();
                return;
            }
            
            var commands = character.StatusController.Commands;
            for (int i = 0; i < commands.Length; i++)
            {
                var button = this.buttons[i];
                var command = commands[i];
                button.gameObject.SetActive(true);
                button.SetProperty(character, command);
            }
            for (int i = commands.Length; i < this.buttons.Count; i++)
            {
                this.buttons[i].gameObject.SetActive(false);
            }
        }

        private void OnSelectCommandFromEnemy()
        {
            this.SetDeactiveAllButton();
        }

        private void SetDeactiveAllButton()
        {
            foreach(var b in this.buttons)
            {
                b.gameObject.SetActive(false);
            }
        }
    }
}
