using UnityEngine;
using System.Collections.Generic;
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
            Broker.Global.Receive<NextTurn>()
                .Where(n => n.NextCharacter.CharacterType == Constants.CharacterType.Player)
                .Subscribe(this.OnNextTurnFromPlayer)
                .AddTo(this);
            Broker.Global.Receive<NextTurn>()
                .Where(n => n.NextCharacter.CharacterType == Constants.CharacterType.Enemy)
                .Subscribe(this.OnNextTurnFromEnemy)
                .AddTo(this);
        }

        private void OnNextTurnFromPlayer(NextTurn eventData)
        {
            var commands = eventData.NextCharacter.StatusController.Commands;
            for (int i = 0; i < commands.Length; i++)
            {
                var button = this.buttons[i];
                var command = commands[i];
                button.gameObject.SetActive(true);
                button.SetProperty(eventData.NextCharacter, command);
            }
            for (int i = commands.Length; i < this.buttons.Count; i++)
            {
                this.buttons[i].gameObject.SetActive(false);
            }
        }

        private void OnNextTurnFromEnemy(NextTurn eventData)
        {
            foreach(var b in this.buttons)
            {
                b.gameObject.SetActive(false);
            }
        }
    }
}
