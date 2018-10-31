using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Tweens
{
    /// <summary>
    /// スケールアニメーションによる強調アニメーション
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Tweens/EmphasisScale")]
    public sealed class EmphasisScaleTween : ScriptableObject
    {
        [SerializeField]
        private float duration;

        [SerializeField]
        private float scale;

        public Tween Apply(Transform transform)
        {
            return transform.DOScale(Vector3.one * this.scale, this.duration)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}
