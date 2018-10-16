using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.UI.PopupControllers
{
    /// <summary>
    /// ポップアップを管理するクラス
    /// </summary>
    [RequireComponent(typeof(Image))]
    public sealed class PopupManager : MonoBehaviour
    {
        [SerializeField]
        private SimplePopupController basicPopupPrefab;

        public static PopupManager Instance { get; private set; }

        private RectTransform cachedTransform;

        private Image background;

        private List<IPopup> popups = new List<IPopup>();

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
        public static T Show<T>(T prefab) where T : PopupBase
        {
            Instance.background.enabled = true;
            var result = Instantiate(prefab, Instance.cachedTransform, false);
            Instance.popups.Add(result);

            return result;
        }

        /// <summary>
        /// 最も手前に存在するポップアップを閉じる
        /// </summary>
        public static void Close()
        {
            Assert.IsTrue(Instance.popups.Count > 0, "ポップアップが存在しません");
            var index = Instance.popups.Count - 1;
            var popup = Instance.popups[index];
            Instance.popups.RemoveAt(index);
            Instance.background.enabled = Instance.popups.Count != 0;
            popup.Close();
        }

        /// <summary>
        /// 指定したポップアップを閉じる
        /// </summary>
        public static void Close(IPopup target)
        {
            Assert.IsTrue(Instance.popups.Count > 0, "ポップアップが存在しません");
            var popup = Instance.popups.Find(p => p == target);
            Assert.IsNotNull(popup);
            Instance.popups.Remove(popup);
            Instance.background.enabled = Instance.popups.Count != 0;
            popup.Close();
        }

        /// <summary>
        /// シンプルなポップアップを表示する
        /// </summary>
        public static SimplePopupController ShowSimplePopup(string title, string message, params string[] buttonNames)
        {
            var result = Show(Instance.basicPopupPrefab);
            result.Setup(title, message, buttonNames);

            return result;
        }
    }
}
