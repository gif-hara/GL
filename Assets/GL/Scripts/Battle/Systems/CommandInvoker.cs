using HK.Framework.EventSystems;
using HK.GL.Events.Battle;
using UniRx;
using UnityEngine;

namespace GL.Scripts.Battle.Systems
{
    /// <summary>
    /// 行動するキャラクターのコマンドを実行するヤーツ
    /// </summary>
    public sealed class CommandInvoker : MonoBehaviour
    {
        void Awake()
        {
            Broker.Global.Receive<NextTurn>()
                .Where(n => n.NextCharacter.CharacterType == Constants.CharacterType.Enemy)
                .Subscribe(this.OnInvokeCommandFromEnemy)
                .AddTo(this);

            Broker.Global.Receive<InvokeCommand>()
                .Subscribe(this.OnInvokeCommand)
                .AddTo(this);
        }

        private void OnInvokeCommand(InvokeCommand eventData)
        {
            var character = eventData.Invoker;
            eventData.Implement.Invoke(character);
        }

        private void OnInvokeCommandFromEnemy(NextTurn eventData)
        {
            // TODO:敵のAIからコマンドを選択する
            var character = eventData.NextCharacter;
            var commands = character.StatusController.Commands;
            var command = commands[Random.Range(0, commands.Length)];
            command.Invoke(character);
        }
    }
}
