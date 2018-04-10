using UnityEngine;

namespace GL.Scripts.Home.UI
{
    /// <summary>
    /// メインパネルを制御するクラス
    /// </summary>
    public sealed class MainPanelController : MonoBehaviour
    {
        private RectTransform rectTransform;

        private Vector2 size;
        
        void Start()
        {
            this.rectTransform = (RectTransform) this.transform;
            this.size = new Vector2(this.rectTransform.rect.width, this.rectTransform.rect.height);
            Debug.Log(this.rectTransform.anchoredPosition);
        }

        public void ChangeRoot(int index)
        {
            this.rectTransform.anchoredPosition = new Vector2(-this.size.x * index, 0.0f);
        }
    }
}
