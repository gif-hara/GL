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
        public enum Mode
        {
            Weapon,
            Accessory,
        }

        [SerializeField]
        private StringAsset.Finder noneEquipmentName;

        [SerializeField]
        private Text equipmentName;

        [SerializeField]
        private Button button;

        private Mode mode;

        private Constants.HandType handType;

        private int accessoryIndex;

        private CharacterDetailsPopupController controller;

        void Start()
        {
            this.button.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    if(_this.mode == Mode.Weapon)
                    {
                        _this.controller.ShowEditEquipmentPopup(_this.handType);
                    }
                    else
                    {
                        _this.controller.ShowEditAccessoryPopup(_this.accessoryIndex);
                    }
                })
                .AddTo(this);
        }

        public void SetupAsWeapon(CharacterDetailsPopupController controller, EquipmentRecord equipment, Constants.HandType handType)
        {
            this.Setup(controller, equipment);
            this.mode = Mode.Weapon;
            this.handType = handType;
        }

        public void SetupAsAccessory(CharacterDetailsPopupController controller, EquipmentRecord equipment, int accessoryIndex)
        {
            this.Setup(controller, equipment);
            this.mode = Mode.Accessory;
            this.accessoryIndex = accessoryIndex;
        }

        private void Setup(CharacterDetailsPopupController controller, EquipmentRecord equipment)
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
