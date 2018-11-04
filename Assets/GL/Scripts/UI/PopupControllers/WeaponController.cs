using GL.Database;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.UI
{
    /// <summary>
    /// 武器UIを制御するクラス
    /// </summary>
    public sealed class WeaponController : MonoBehaviour
    {
        [SerializeField]
        private Text weaponName;

        [SerializeField]
        private Button button;
        public Button Button => this.button;

        [SerializeField]
        private Transform rankParent;

        [SerializeField]
        private CommandUIController commandPrefab;

        [SerializeField]
        private GameObject rankPrefab;

        public WeaponController Setup(EquipmentRecord weapon)
        {
            this.weaponName.text = weapon.EquipmentName;
            foreach(var c in weapon.Commands)
            {
                Instantiate(this.commandPrefab, this.transform).Setup(c);
            }

            for (var i = 0; i < weapon.Rank; i++)
            {
                Instantiate(this.rankPrefab, this.rankParent, false);
            }

            return this;
        }
    }
}
