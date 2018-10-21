using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.UI.PopupControllers
{
    /// <summary>
    /// 勝利時のリザルトポップアップを制御するクラス
    /// </summary>
    public sealed class ResultWinPopupController : PopupBase
    {
        public enum SubmitType
        {
            ToHome,
        }

        [SerializeField]
        private Button toHomeButton;

        public ResultWinPopupController Setup()
        {
            this.OnClickSubmit(this.toHomeButton, (int)SubmitType.ToHome);
            return this;
        }
    }
}
