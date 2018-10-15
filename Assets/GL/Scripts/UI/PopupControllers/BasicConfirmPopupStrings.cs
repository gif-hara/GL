using System;
using System.Linq;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.UI.PopupControllers
{
    /// <summary>
    /// シンプルなポップアップを表示するために必要な文字列を管理するクラス
    /// </summary>
    [Serializable]
    public sealed class BasicConfirmPopupStrings
    {
        public StringAsset.Finder Title;

        public StringAsset.Finder Message;

        public StringAsset.Finder[] ButtonNames;

        public BasicConfirmPopupController Show()
        {
            return PopupController.ShowBasicPopup(this.Title.Get, this.Message.Get, this.ButtonNames.Select(b => b.Get).ToArray());
        }

        public BasicConfirmPopupController Show(
            Func<StringAsset.Finder, string> titleSelector,
            Func<StringAsset.Finder, string> messageSelector,
            Func<StringAsset.Finder[], string[]> buttonNamesSelector
            )
        {
            return PopupController.ShowBasicPopup(titleSelector(this.Title), messageSelector(this.Message), buttonNamesSelector(this.ButtonNames));
        }
    }
}
