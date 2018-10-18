using GL.UI.PopupControllers;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// キャラクター詳細ポップアップの武器ボタンを制御するクラス
    /// </summary>
    public sealed class CharacterDetailsWeaponButtonController : MonoBehaviour
    {
        [SerializeField]
        private Constants.HandType handType;

        [SerializeField]
        private CharacterDetailsPopupController popupController;

        [SerializeField]
        private Button button;

        void Start()
        {
            this.button.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.popupController.ShowEquippedWeaponPopup(_this.handType);
                })
                .AddTo(this);
        }
    }
}
