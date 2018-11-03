using GL.Database;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.UI
{
    /// <summary>
    /// アクセサリーUIを制御するクラス
    /// </summary>
    public sealed class AccessoryUIController : MonoBehaviour
    {
        [SerializeField]
        private Text accessoryName;

        [SerializeField]
        private Button button;
        public Button Button => this.button;

        [SerializeField]
        private Transform rankParent;

        [SerializeField]
        private AccessoryElementUIController accessoryElementPrefab;

        [SerializeField]
        private CommandUIController commandPrefab;

        [SerializeField]
        private GameObject rankPrefab;

        public AccessoryUIController Setup(AccessoryRecord accessory)
        {
            this.accessoryName.text = accessory.AccessoryName;
            foreach(var a in accessory.Elements)
            {
                Instantiate(this.accessoryElementPrefab, this.transform).Setup(a);
            }
            foreach(var c in accessory.Commands)
            {
                Instantiate(this.commandPrefab, this.transform).Setup(c);
            }

            for (var i = 0; i < accessory.Rank; i++)
            {
                Instantiate(this.rankPrefab, this.rankParent, false);
            }

            return this;
        }
    }
}
