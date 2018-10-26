using System;
using DG.Tweening;
using GL.Tweens;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.UI
{
    /// <summary>
    /// キャラクターUIのアニメーションを制御するクラス
    /// </summary>
    public sealed class CharacterUIAnimation : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        [SerializeField]
        private TargetableTween targetableTween;

        [SerializeField]
        private AttackTween attackTween;

        private Tween currentTween;

        public void StartTargetableAnimation()
        {
            this.ClearTween();
            this.currentTween = this.targetableTween.Apply(this.target);
        }

        public void StartAttackAnimation(Action onAttack, bool isReverse)
        {
            this.ClearTween();
            this.attackTween.Apply(this.target, onAttack, isReverse);
        }

        public void ClearTween()
        {
            if(this.currentTween == null)
            {
                return;
            }

            this.currentTween.Kill();
            this.currentTween = null;
        }
    }
}
