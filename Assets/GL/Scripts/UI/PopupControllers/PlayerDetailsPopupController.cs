using GL.Scripts.User;
using UnityEngine;
using UnityEngine.UI;

namespace GL.Scripts.UI.PopupControllers
{
    /// <summary>
    /// プレイヤー詳細ポップアップを制御するクラス
    /// </summary>
    public sealed class PlayerDetailsPopupController : MonoBehaviour
    {
        [SerializeField]
        private Text characterName;
        
        public void Setup(Player player)
        {
            this.characterName.text = player.PlayerName;
        }
    }
}
