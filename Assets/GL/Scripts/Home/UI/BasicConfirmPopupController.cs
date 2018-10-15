using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// シンプルなポップアップを制御するクラス
    /// </summary>
    public sealed class BasicConfirmPopupController : MonoBehaviour
    {
        [SerializeField]
        private BasicConfirmPopupButtonController buttonPrefab;

        [SerializeField]
        private Transform buttonParent;

        [SerializeField]
        private Text title;

        [SerializeField]
        private Text message;

        public Subject<int> Submit { get; private set; } = new Subject<int>();

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
                        _this.Submit.OnNext(i);
                    });
                button.Text.text = buttonNames[i];
            }
        }
    }
}
