using GL.Database;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.UI
{
    /// <summary>
    /// 武器UIを制御するクラス
    /// </summary>
    public sealed class EquipmentUIController : MonoBehaviour
    {
        [SerializeField]
        private Text equipmentName;

        [SerializeField]
        private Button button;
        public Button Button => this.button;

        [SerializeField]
        private Transform rankParent;

        [SerializeField]
        private CommandUIController commandPrefab;

        [SerializeField]
        private SkillElementUIController accessoryElementPrefab;

        [SerializeField]
        private GameObject rankPrefab;

        public EquipmentUIController Setup(EquipmentRecord equipment)
        {
            this.equipmentName.text = equipment.EquipmentName;
            foreach(var c in equipment.Commands)
            {
                Instantiate(this.commandPrefab, this.transform).Setup(c);
            }
            foreach(var s in equipment.SkillElements)
            {
                Instantiate(this.accessoryElementPrefab, this.transform).Setup(s);
            }

            for (var i = 0; i < equipment.Rank; i++)
            {
                Instantiate(this.rankPrefab, this.rankParent, false);
            }

            return this;
        }
    }
}
