using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// シンプルなポップアップのボタンを制御するクラス
    /// </summary>
    public sealed class BasicConfirmPopupButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button;
        public Button Button => this.button;

        [SerializeField]
        private Text text;
        public Text Text => this.text;
    }
}
