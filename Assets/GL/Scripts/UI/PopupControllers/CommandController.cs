using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.UI
{
    /// <summary>
    /// UIのコマンドにプロパティを設定するクラス
    /// </summary>
    public sealed class CommandController : MonoBehaviour
    {
        [SerializeField]
        private Text commandName;

        [SerializeField]
        private Text description;

        public void Setup(Battle.Commands.Bundle.Blueprint command)
        {
            this.commandName.text = command.Parameter.Name.Get;
            this.description.text = command.Parameter.Description.Get;
        }
    }
}
