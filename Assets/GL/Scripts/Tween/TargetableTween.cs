using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Tweens
{
    /// <summary>
    /// ターゲット可能なことを表現するアニメーション
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Tweens/Targetable")]
    public sealed class TargetableTween : ScriptableObject
    {
        [SerializeField]
        private float rotationZ;

        [SerializeField]
        private float durationMin;

        [SerializeField]
        private float durationMax;

        [SerializeField]
        private Ease ease;

        public Tween Apply(Transform transform)
        {
            var rotation = transform.localRotation;
            var duration = Random.Range(this.durationMin, this.durationMax);
            return DOTween.Sequence()
                .Append(transform.DOLocalRotate(Vector3.forward * this.rotationZ, duration).SetEase(this.ease))
                .Append(transform.DOLocalRotate(Vector3.forward * -this.rotationZ, duration).SetEase(this.ease))
                .SetLoops(-1, LoopType.Yoyo)
                .OnKill(() => transform.localRotation = rotation);
        }
    }
}
