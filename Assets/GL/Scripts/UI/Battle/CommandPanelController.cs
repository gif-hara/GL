using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.GL.Events;
using HK.GL.Events.Battle;
using UniRx;
using HK.GL.Battle;

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
            GLEvent.GlobalBroker.Receive<NextTurn>()
                .Where(n => n.NextCharacter.CharacterType == Constants.CharacterType.Player)
                .Subscribe(this.OnNextTurnFromPlayer)
                .AddTo(this);
            GLEvent.GlobalBroker.Receive<NextTurn>()
                .Where(n => n.NextCharacter.CharacterType == Constants.CharacterType.Enemy)
                .Subscribe(this.OnNextTurnFromEnemy)
                .AddTo(this);
        }

        private void OnNextTurnFromPlayer(NextTurn eventData)
        {
            var commands = eventData.NextCharacter.Status.Commands;
            for(int i=0; i<commands.Count; i++)
            {
                var button = this.buttons[i];
                var command = commands[i];
                button.gameObject.SetActive(true);
                button.SetProperty(eventData.NextCharacter, command);
            }
            for(int i=commands.Count; i<this.buttons.Count; i++)
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
