using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Scripts.UI.PopupControllers
{
    /// <summary>
    /// ポップアップを制御するクラス
    /// </summary>
    [RequireComponent(typeof(Image))]
    public sealed class PopupController : MonoBehaviour
    {
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
        /// ポップアップを生成する
        /// </summary>
        public new T Instantiate<T>(T prefab) where T : Component
        {
            Assert.IsNull(this.currentPopup, "すでにポップアップが存在します");
            this.background.enabled = true;
            var result = Instantiate(prefab, this.cachedTransform, false);
            this.currentPopup = result.gameObject;

            return result;
        }

        public void Close()
        {
            Assert.IsNotNull(this.currentPopup, "ポップアップが存在しません");
            this.background.enabled = false;
            Destroy(this.currentPopup);
        }
    }
}
