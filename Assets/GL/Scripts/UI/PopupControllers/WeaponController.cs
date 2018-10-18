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
        private CommandController commandPrefab;

        public void Setup(Battle.Weapon weapon)
        {
            this.weaponName.text = weapon.WeaponName;
            foreach(var c in weapon.Commands)
            {
                Instantiate(this.commandPrefab, this.transform).Setup(c);
            }
        }
    }
}
