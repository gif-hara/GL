using UnityEngine;
using UnityEngine.Assertions;
using static GL.Battle.Constants;

namespace GL.Home.UI
{
    /// <summary>
    /// ショップのパネルを制御するクラス
    /// </summary>
    public sealed class ShopPanelController : MonoBehaviour
    {
        [SerializeField]
        private GameObject topRoot;

        [SerializeField]
        private GameObject listRoot;

        [SerializeField]
        private RectTransform listContents;

        /// <summary>
        /// <paramref name="weaponType"/>に対応した武器リストを表示する
        /// </summary>
        public void ShowWeaponList(WeaponType weaponType)
        {
            this.topRoot.SetActive(false);
            this.listRoot.SetActive(true);
        }

        /// <summary>
        /// トップ画面を表示する
        /// </summary>
        public void ShowTop()
        {
            this.topRoot.SetActive(true);
            this.listRoot.SetActive(false);
        }
    }
}
