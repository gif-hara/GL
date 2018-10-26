using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Tweens
{
    /// <summary>
    /// ダメージアニメーション
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Tweens/Damage")]
    public sealed class DamageTween : ScriptableObject
    {
        [SerializeField]
        private float duration;

        [SerializeField]
        private float strength;

        [SerializeField]
        private int vibrato;

        [SerializeField]
        private float randomness;

        [SerializeField]
        private bool snapping;

        [SerializeField]
        private bool fadeOut;

        public Tween Apply(Transform transform)
        {
            return transform.DOShakePosition(this.duration, this.strength, this.vibrato, this.randomness, this.snapping, this.fadeOut);
        }
    }
}
