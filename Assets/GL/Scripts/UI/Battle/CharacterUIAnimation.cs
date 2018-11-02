using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using GL.Battle.CharacterControllers;
using GL.Tweens;
using HK.GL.Extensions;
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
            EmphasisScale,
        }

        [SerializeField]
        private Transform target;

        [SerializeField]
        private EmphasisScaleTween emphasisScaleTween;

        [SerializeField]
        private TargetableTween targetableTween;

        [SerializeField]
        private AttackTween attackTween;

        [SerializeField]
        private DamageTween damageTween;

        private Dictionary<AnimationType, Tween> tweens = new Dictionary<AnimationType, Tween>();

        private Subject<AnimationType> completeStream = new Subject<AnimationType>();
        public IObservable<AnimationType> OnCompleteAsObservable() => this.completeStream;

        public void StartEmphasisScaleAnimation()
        {
            this.ChangeTween(this.emphasisScaleTween.Apply(this.target), AnimationType.EmphasisScale);
        }

        public void StartTargetableAnimation()
        {
            this.ChangeTween(this.targetableTween.Apply(this.target), AnimationType.Targetable);
        }

        public IObservable<AnimationType> StartAttackAnimation(Action onAttack, bool isReverse)
        {
            this.ClearTween();
            this.ChangeTween(this.attackTween.Apply(this.target, onAttack, isReverse), AnimationType.Attack);

            return this.OnCompleteAsObservable();
        }

        public void StartDamageAnimation()
        {
            this.ChangeTween(this.damageTween.Apply(this.target), AnimationType.Damage);
        }

        public bool IsPlay(AnimationType type)
        {
            return this.tweens.ContainsKey(type) && this.tweens[type] != null;
        }

        public void KillTween(AnimationType type)
        {
            if (!this.tweens.ContainsKey(type) || this.tweens[type] == null)
            {
                return;
            }

            this.tweens[type].Kill();
        }

        public void ClearTween()
        {
            var tweens = this.tweens.Select(p => p.Value).Where(t => t != null).ToArray();
            tweens.ForEach(t => t.Kill());
        }

        private void ChangeTween(Tween tween, AnimationType type)
        {
            this.KillTween(type);
            if(!this.tweens.ContainsKey(type))
            {
                this.tweens.Add(type, tween);
            }
            else
            {
                this.tweens[type] = tween;
            }

            tween.OnKill(() =>
            {
                this.tweens[type] = null;
                this.completeStream.OnNext(type);
                this.target.localPosition = Vector3.zero;
                this.target.localScale = Vector3.one;
                this.target.localRotation = Quaternion.identity;
            });
        }
    }
}
