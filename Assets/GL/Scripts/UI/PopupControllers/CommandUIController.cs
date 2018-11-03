using GL.Database;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.UI
{
    /// <summary>
    /// UIのコマンドにプロパティを設定するクラス
    /// </summary>
    public sealed class CommandUIController : MonoBehaviour
    {
        [SerializeField]
        private Text commandName;

        [SerializeField]
        private Text description;

        public void Setup(ConditionalCommandRecord command)
        {
            this.commandName.text = command.CommandRecord.Parameter.Name.Get;
            this.description.text = command.CommandRecord.Parameter.Description.Get;
        }
    }
}
