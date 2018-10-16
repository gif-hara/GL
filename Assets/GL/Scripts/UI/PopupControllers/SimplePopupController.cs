using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.UI.PopupControllers
{
    /// <summary>
    /// シンプルなポップアップを制御するクラス
    /// </summary>
    public sealed class SimplePopupController : PopupBase
    {
        [SerializeField]
        private SimplePopupButtonController buttonPrefab;

        [SerializeField]
        private Transform buttonParent;

        [SerializeField]
        private Text title;

        [SerializeField]
        private Text message;

        public void Setup(string title, string message, params string[] buttonNames)
        {
            this.title.text = title;
            this.message.text = message;
            
            for (var i = 0; i < buttonNames.Length; i++)
            {
                var button = Instantiate(this.buttonPrefab, this.buttonParent, false);
                button.Button.OnClickAsObservable()
                    .SubscribeWithState(this, (_, _this) =>
                    {
                        _this.submit.OnNext(i);
                    });
                button.Text.text = buttonNames[i];
            }
        }
    }
}
