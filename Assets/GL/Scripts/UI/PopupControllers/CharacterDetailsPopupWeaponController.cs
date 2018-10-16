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
        private StringAsset.Finder weaponNameFormat;

        [SerializeField]
        private StringAsset.Finder noneWeaponName;

        [SerializeField]
        private Text weaponName;

        [SerializeField]
        CommandController commandPrefab;

        public void Setup(Battle.Weapon weapon)
        {
            if(weapon == null)
            {
                this.weaponName.text = this.weaponNameFormat.Format(this.noneWeaponName.Get);
            }
            else
            {
                this.weaponName.text = this.weaponNameFormat.Format(weapon.WeaponName);
                weapon.Commands.ForEach(c =>
                {
                    var commandUI = Instantiate(this.commandPrefab, this.transform, false);
                    commandUI.Setup(c);
                });
            }
        }
    }
}
