using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.UI.PopupControllers
{
    /// <summary>
    /// ポップアップのインターフェイス
    /// </summary>
    public interface IPopup
    {
        /// <summary>
        /// ポップアップでのユーザーのアクションを返すオブザーバー
        /// </summary>
        IObservable<int> SubmitAsObservable();

        /// <summary>
        /// ポップアップを開く際の処理
        /// </summary>
        void Open();

        /// <summary>
        /// ポップアップを閉じる際の処理
        /// </summary>
        void Close();
    }
}
