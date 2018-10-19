using GL.Home.UI;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Events.Home
{
    /// <summary>
    /// フッターのモードを切り替えるイベント
    /// </summary>
    public sealed class ChangeFooter : Message<ChangeFooter, FooterController.Mode>
    {
        /// <summary>
        /// 切り替えたいモード
        /// </summary>
        public FooterController.Mode Mode => this.param1;
    }
}
