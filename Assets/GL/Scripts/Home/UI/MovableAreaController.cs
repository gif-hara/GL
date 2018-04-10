using DG.Tweening;
using UnityEngine;

namespace GL.Scripts.Home.UI
{
    /// <summary>
    /// 移動可能エリアを制御するクラス
    /// </summary>
    public sealed class MovableAreaController : MonoBehaviour
    {
        [SerializeField]
        private int initialIndex;
        
        [SerializeField]
        private float changeDuration;

        [SerializeField]
        private Ease changeEase;
        
        private RectTransform rectTransform;

        private Vector2 size;
        
        void Start()
        {
            this.rectTransform = (RectTransform) this.transform;
            this.size = new Vector2(this.rectTransform.rect.width, this.rectTransform.rect.height);
            this.ChangeRootImmediate(this.initialIndex);
        }

        public void ChangeRoot(int index)
        {
            this.rectTransform
                .DOAnchorPos(this.GetAnchorPosition(index), this.changeDuration, true)
                .SetEase(this.changeEase);
        }

        public void ChangeRootImmediate(int index)
        {
            this.rectTransform.anchoredPosition = this.GetAnchorPosition(index);
        }

        private Vector2 GetAnchorPosition(int index)
        {
            return new Vector2(this.size.x * index, 0.0f);
        }
    }
}
