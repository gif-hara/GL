using System.Collections.Generic;
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

        private List<GameObject> popups = new List<GameObject>();

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
            Instance.background.enabled = true;
            var result = Instantiate(prefab, Instance.cachedTransform, false);
            Instance.popups.Add(result.gameObject);

            return result;
        }

        public static void Close()
        {
            Assert.IsTrue(Instance.popups.Count > 0, "ポップアップが存在しません");
            var index = Instance.popups.Count - 1;
            var popup = Instance.popups[index];
            Instance.popups.RemoveAt(index);
            Instance.background.enabled = Instance.popups.Count != 0;
            Destroy(popup);
        }

        public static void Close(GameObject target)
        {
            Assert.IsTrue(Instance.popups.Count > 0, "ポップアップが存在しません");
            var popup = Instance.popups.Find(p => p == target);
            Assert.IsNotNull(popup);
            Instance.background.enabled = Instance.popups.Count != 0;
            Destroy(popup);
        }

        public static SimplePopupController ShowSimplePopup(string title, string message, params string[] buttonNames)
        {
            var result = Show(Instance.basicPopupPrefab);
            result.Setup(title, message, buttonNames);

            return result;
        }
    }
}
