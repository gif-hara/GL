using GL.User;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// ホームのプレイヤーボタンを制御するクラス
    /// </summary>
    public sealed class PlayerButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button;
        public Button Button { get { return button; } }

        [SerializeField]
        private Text text;

        public readonly CompositeDisposable OnClickObserver = new CompositeDisposable();

        public void SetProperty(Player player)
        {
            this.text.text = player.Blueprint.CharacterName;
            this.OnClickObserver.Clear();
        }
    }
}
