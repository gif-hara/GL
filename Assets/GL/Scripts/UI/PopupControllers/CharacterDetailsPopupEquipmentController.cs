using GL.Database;
using HK.Framework.Text;
using HK.GL.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// <see cref="CharacterDetailsPopupController"/>の装備品部分を制御するクラス
    /// </summary>
    public sealed class CharacterDetailsPopupEquipmentController : MonoBehaviour
    {
        [SerializeField]
        private StringAsset.Finder noneEquipmentName;

        [SerializeField]
        private Text equipmentName;

        [SerializeField]
        private Constants.HandType handType;

        [SerializeField]
        private Button button;

        private CharacterDetailsPopupController controller;

        void Start()
        {
            this.button.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.controller.ShowEquippedWeaponPopup(_this.handType);
                })
                .AddTo(this);
        }

        public void Setup(CharacterDetailsPopupController controller, EquipmentRecord equipment)
        {
            if (equipment == null)
            {
                this.equipmentName.text = this.noneEquipmentName.Get;
            }
            else
            {
                this.equipmentName.text = equipment.EquipmentName;
            }

            this.controller = controller;
        }
    }
}
