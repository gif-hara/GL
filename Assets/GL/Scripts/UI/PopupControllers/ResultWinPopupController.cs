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
        private Text experience;

        [SerializeField]
        private Text gold;

        [SerializeField]
        private Button toHomeButton;

        public ResultWinPopupController Setup(int experience, int gold)
        {
            this.experience.text = experience.ToString();
            this.gold.text = gold.ToString();
            this.OnClickSubmit(this.toHomeButton, (int)SubmitType.ToHome);
            return this;
        }
    }
}
