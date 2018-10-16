using GL.Home.UI;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.UI.PopupControllers
{
    /// <summary>
    /// ポップアップを制御するクラス
    /// </summary>
    [RequireComponent(typeof(Image))]
    public sealed class PopupController : MonoBehaviour
    {
        [SerializeField]
        private SimplePopupController basicPopupPrefab;

        public static PopupController Instance { get; private set; }

        private RectTransform cachedTransform;

        private Image background;

        private GameObject currentPopup;

        void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;

            this.cachedTransform = (RectTransform) this.transform;
            this.background = this.GetComponent<Image>();
            this.background.enabled = false;
        }

        void OnDestroy()
        {
            Assert.IsNotNull(Instance);
            Instance = null;
        }

        /// <summary>
        /// ポップアップを表示する
        /// </summary>
        public static T Show<T>(T prefab) where T : Component
        {
            Assert.IsNull(Instance.currentPopup, "すでにポップアップが存在します");
            Instance.background.enabled = true;
            var result = Instantiate(prefab, Instance.cachedTransform, false);
            Instance.currentPopup = result.gameObject;

            return result;
        }

        public static void Close()
        {
            Assert.IsNotNull(Instance.currentPopup, "ポップアップが存在しません");
            Instance.background.enabled = false;
            Destroy(Instance.currentPopup);
            Instance.currentPopup = null;
        }

        public static SimplePopupController ShowSimplePopup(string title, string message, params string[] buttonNames)
        {
            var result = Show(Instance.basicPopupPrefab);
            result.Setup(title, message, buttonNames);

            return result;
        }
    }
}
