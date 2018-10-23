using System.Collections.Generic;
using GL.Database;
using HK.GL.Extensions;
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

        [SerializeField]
        private Transform materialListParent;

        [SerializeField]
        private MaterialElement materialElement;

        public ResultWinPopupController Setup(int experience, int gold, Dictionary<MaterialRecord, int> materials)
        {
            this.experience.text = experience.ToString();
            this.gold.text = gold.ToString();
            materials.ForEach(p => Instantiate(this.materialElement, this.materialListParent, false).Setup(p.Key, p.Value));
            this.OnClickSubmit(this.toHomeButton, (int)SubmitType.ToHome);
            return this;
        }
    }
}
