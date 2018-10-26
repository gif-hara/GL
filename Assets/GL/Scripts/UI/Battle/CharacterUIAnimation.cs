using System;
using DG.Tweening;
using GL.Battle.CharacterControllers;
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
            None,
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

        private AnimationType currentType = AnimationType.None;

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
            if(this.currentTween == null || this.currentType == AnimationType.Attack)
            {
                return;
            }

            this.currentTween.Kill();
            this.target.localRotation = Quaternion.identity;
            this.currentTween = null;
            this.currentType = AnimationType.None;
        }

        public bool IsPlay => this.currentTween != null;

        private void ChangeTween(Tween tween, AnimationType type)
        {
            // 攻撃アニメーション中は切り替えられない
            if(this.currentType == AnimationType.Attack)
            {
                return;
            }
            this.ClearTween();
            this.currentType = type;
            this.currentTween = tween;
            this.currentTween.OnKill(() =>
            {
                this.completeStream.OnNext(type);
                this.currentTween = null;
                this.currentType = AnimationType.None;
                this.target.localPosition = Vector3.zero;
            });
        }
    }
}
