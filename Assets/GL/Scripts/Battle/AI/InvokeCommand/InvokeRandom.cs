using GL.Battle.CharacterControllers;
using GL.Events.Battle;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.AIControllers
{
    /// <summary>
    /// ランダムにコマンドを実行する
    /// </summary>
    [CreateAssetMenu(menuName = "GL/AI/InvokeCommand/InvokeRandom")]
    public sealed class InvokeRandom : InvokeCommand
    {
        public override void Invoke(Character character)
        {
            var commands = character.StatusController.Commands;
            var command = commands[Random.Range(0, commands.Length)];
            Broker.Global.Publish(SelectedCommand.Get(character, command));
        }
    }
}
