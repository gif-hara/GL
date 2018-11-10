using GL.Database;
using GL.Extensions;
using HK.Framework.Text;
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

        [SerializeField]
        private Text condition;

        [SerializeField]
        private StringAsset.Finder conditionFormat;

        [SerializeField]
        private Text chargeTurn;

        [SerializeField]
        private Text initialChargeTurn;

        public void Setup(ConditionalCommandRecord command)
        {
            this.commandName.text = command.CommandRecord.Parameter.Name.Get;
            this.description.text = command.CommandRecord.Parameter.Description.Get;
            this.condition.text = this.conditionFormat.Format(command.Condition.Description).RemoveLastNewLine();
            this.chargeTurn.text = command.CommandRecord.Parameter.ChargeTurn.ToString();
            this.initialChargeTurn.text = command.CommandRecord.Parameter.InitialChargeTurn.ToString();
        }
    }
}
