using UnityEngine;

namespace GL.Scripts.Home.UI
{
    /// <summary>
    /// メインパネルを制御するクラス
    /// </summary>
    public sealed class MainPanelController : MonoBehaviour
    {
        void Start()
        {
            var rectTransform = (RectTransform) this.transform;
            var width = rectTransform.rect.width;
            for (int i = 0; i < rectTransform.childCount; i++)
            {
                var child = (RectTransform) rectTransform.GetChild(i);
                var pos = child.anchoredPosition;
                pos.x = width * i;
                child.anchoredPosition = pos;
                child.gameObject.SetActive(true);
            }
        }
    }
}
