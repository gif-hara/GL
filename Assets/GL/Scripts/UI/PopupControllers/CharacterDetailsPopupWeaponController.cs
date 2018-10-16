using HK.Framework.Text;
using HK.GL.Extensions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.UI.PopupControllers
{
    /// <summary>
    /// <see cref="CharacterDetailsPopupController"/>の武器部分を制御するクラス
    /// </summary>
    public sealed class CharacterDetailsPopupWeaponController : MonoBehaviour
    {
        [SerializeField]
        private StringAsset.Finder noneWeaponName;

        [SerializeField]
        private Text weaponName;

        public void Setup(Battle.Weapon weapon)
        {
            if(weapon == null)
            {
                this.weaponName.text = this.noneWeaponName.Get;
            }
            else
            {
                this.weaponName.text = weapon.WeaponName;
            }
        }
    }
}
