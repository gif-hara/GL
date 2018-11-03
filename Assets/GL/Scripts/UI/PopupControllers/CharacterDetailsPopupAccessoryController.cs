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
    /// <see cref="CharacterDetailsPopupController"/>のアクセサリー部分を制御するクラス
    /// </summary>
    public sealed class CharacterDetailsPopupAccessoryController : MonoBehaviour
    {
        [SerializeField]
        private StringAsset.Finder noneAccessoryName;

        [SerializeField]
        private Text accessoryName;

        [SerializeField]
        private Button button;

        private CharacterDetailsPopupController controller;

        private int index;

        void Start()
        {
            this.button.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.controller.ShowEquippedAccessoryPopup(_this.index);
                })
                .AddTo(this);
        }

        public void Setup(CharacterDetailsPopupController controller, AccessoryRecord accessory, int index)
        {
            if (accessory == null)
            {
                this.accessoryName.text = this.noneAccessoryName.Get;
            }
            else
            {
                this.accessoryName.text = accessory.AccessoryName;
            }

            this.controller = controller;
            this.index = index;
        }
    }
}
