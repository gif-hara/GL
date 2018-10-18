using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.UI
{
    /// <summary>
    /// コマンドリストUIを制御するクラス
    /// </summary>
    public sealed class CommandListController : MonoBehaviour
    {
        [SerializeField]
        private Transform parent;

        [SerializeField]
        private CommandController commandPrefab;

        private readonly List<CommandController> commands = new List<CommandController>();

        public void Setup(IEnumerable<Battle.Commands.Bundle.Blueprint> commands)
        {
            this.Clear();
            foreach(var c in commands)
            {
                var command = Instantiate(this.commandPrefab, this.parent, false);
                command.Setup(c);
            }
        }

        public void Clear()
        {
            for (var i = 0; i < this.commands.Count; i++)
            {
                Destroy(this.commands[i].gameObject);
            }
        }
    }
}
