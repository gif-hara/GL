using System;
using DG.Tweening;
using GL.Tweens;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.UI
{
    /// <summary>
    /// キャラクターUIのアニメーションを制御するクラス
    /// </summary>
    public sealed class CharacterUIAnimation : MonoBehaviour
    {
        public enum AnimationType
        {
            Targetable,
            Attack,
            Damage,
        }

        [SerializeField]
        private Transform target;

        [SerializeField]
        private TargetableTween targetableTween;

        [SerializeField]
        private AttackTween attackTween;

        [SerializeField]
        private DamageTween damageTween;

        private Tween currentTween;

        private Subject<AnimationType> completeStream = new Subject<AnimationType>();
        public IObservable<AnimationType> OnCompleteAsObservable() => this.completeStream;

        public void StartTargetableAnimation()
        {
            this.ChangeTween(this.targetableTween.Apply(this.target), AnimationType.Targetable);
        }

        public void StartAttackAnimation(Action onAttack, bool isReverse)
        {
            this.ChangeTween(this.attackTween.Apply(this.target, onAttack, isReverse), AnimationType.Attack);
        }

        public void StartDamageAnimation()
        {
            this.ChangeTween(this.damageTween.Apply(this.target), AnimationType.Damage);
        }

        public void ClearTween()
        {
            if(this.currentTween == null)
            {
                return;
            }

            this.target.localRotation = Quaternion.identity;
            this.currentTween.Kill();
            this.currentTween = null;
        }

        private void ChangeTween(Tween tween, AnimationType type)
        {
            this.ClearTween();
            this.currentTween = tween;
            this.currentTween.OnKill(() => this.completeStream.OnNext(type));
        }
    }
}
