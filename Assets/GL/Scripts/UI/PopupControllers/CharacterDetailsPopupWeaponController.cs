using HK.Framework.Text;
using HK.GL.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Home.UI
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

        [SerializeField]
        private Constants.HandType handType;

        [SerializeField]
        private Button button;

        public void Setup(CharacterDetailsPopupController controller, Battle.Weapon weapon)
        {
            if (weapon == null)
            {
                this.weaponName.text = this.noneWeaponName.Get;
            }
            else
            {
                this.weaponName.text = weapon.WeaponName;
            }

            this.button.OnClickAsObservable()
                .SubscribeWithState2(this, controller, (_, _this, _controller) =>
                {
                    _controller.ShowEquippedWeaponPopup(_this.handType);
                })
                .AddTo(this);
        }
    }
}
