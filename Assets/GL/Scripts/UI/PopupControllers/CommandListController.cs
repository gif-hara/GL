using System.Collections.Generic;
using GL.Database;
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
        private CommandUIController commandPrefab;

        private readonly List<CommandUIController> commands = new List<CommandUIController>();

        public void Setup(IEnumerable<ConditionalCommandRecord> commands)
        {
            this.Clear();
            foreach(var c in commands)
            {
                var command = Instantiate(this.commandPrefab, this.parent, false);
                command.Setup(c);
                this.commands.Add(command);
            }
        }

        public void Clear()
        {
            for (var i = 0; i < this.commands.Count; i++)
            {
                Destroy(this.commands[i].gameObject);
            }
            this.commands.Clear();
        }
    }
}
