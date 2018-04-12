using GL.Scripts.User;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GL.Scripts.Home.UI
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

        public void SetProperty(Player player)
        {
            this.text.text = player.Blueprint.CharacterName;
            this.Button.OnClickAsObservable()
                .SubscribeWithState(player, (_, p) =>
                {
                    Debug.Log(string.Format("Player = {0} Id = {1}", p.Blueprint.CharacterName, p.Id));
                })
                .AddTo(this);
        }
    }
}
