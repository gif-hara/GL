using System;
using DG.Tweening;
using HK.GL.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Tweens
{
    /// <summary>
    /// 攻撃アニメーション
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Tweens/Attack")]
    public sealed class AttackTween : ScriptableObject
    {
        [SerializeField]
        private Element[] elements;

        public Tween Apply(Transform transform, Action onAttack, bool isReverse)
        {
            var sequence = DOTween.Sequence();
            this.elements.ForEach(e => sequence.Append(e.Apply(transform, onAttack, isReverse)));

            return sequence;
        }

        [Serializable]
        public class Element
        {
            [SerializeField]
            private Vector3 position;

            [SerializeField]
            private float duration;

            [SerializeField]
            private Ease ease;

            [SerializeField]
            private bool attackTiming;

            public Tween Apply(Transform transform, Action onAttack, bool isReverse)
            {
                var position = isReverse ? this.position * -1.0f : this.position;
                var tween = transform.DOLocalMove(position, this.duration, true).SetEase(ease);
                if(this.attackTiming)
                {
                    tween.OnComplete(() => onAttack());
                }

                return tween;
            }
        }
    }
}
