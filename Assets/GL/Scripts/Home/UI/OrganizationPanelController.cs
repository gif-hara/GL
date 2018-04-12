using GL.Scripts.User;
using UnityEngine;

namespace GL.Scripts.Home.UI
{
    /// <summary>
    /// 編成パネルを制御するクラス
    /// </summary>
    public sealed class OrganizationPanelController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform playersParent;

        [SerializeField]
        private PlayerButtonController[] parties;

        [SerializeField]
        private PlayerButtonController playerButtonPrefab;
        
        void Start()
        {
            var userData = UserData.Instance;
            this.CreatePlayersPanel(userData);
            foreach (var userDataPlayer in userData.Players)
            {
                Debug.Log(userDataPlayer.Id);
            }
        }

        private void CreatePlayersPanel(UserData userData)
        {
            foreach (var player in userData.Players)
            {
                var button = Instantiate(this.playerButtonPrefab, this.playersParent, false);
                button.SetProperty(player);
            }
        }
    }
}
