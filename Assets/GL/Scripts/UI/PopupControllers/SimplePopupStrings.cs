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
    public sealed class SimplePopupStrings
    {
        public StringAsset.Finder Title;

        public StringAsset.Finder Message;

        public StringAsset.Finder[] ButtonNames;

        /// <summary>
        /// ポップアップを表示する
        /// </summary>
        public SimplePopupController Show()
        {
            return PopupManager.ShowSimplePopup(this.Title.Get, this.Message.Get, this.ButtonNames.Select(b => b.Get).ToArray());
        }

        /// <summary>
        /// ポップアップを表示する
        /// </summary>
        public SimplePopupController Show(
            Func<StringAsset.Finder, string> titleSelector = null,
            Func<StringAsset.Finder, string> messageSelector = null,
            Func<StringAsset.Finder[], string[]> buttonNamesSelector = null
            )
        {
            var title = titleSelector == null ? this.Title.Get : titleSelector(this.Title);
            var message = messageSelector == null ? this.Message.Get : messageSelector(this.Message);
            var buttonNames = buttonNamesSelector == null ? this.ButtonNames.Select(b => b.Get).ToArray() : buttonNamesSelector(this.ButtonNames);
            return PopupManager.ShowSimplePopup(title, message, buttonNames);
        }
    }
}
