using System;
using GL.Scripts.Battle.Systems;
using HK.Framework.EventSystems;
using HK.GL.Events.Battle;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Scripts.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクター.
    /// </summary>
    public sealed class Character : MonoBehaviour
    {
        /// <summary>
        /// 基本ステータス
        /// </summary>
        public CharacterStatusController StatusController { private set; get; }

        public Constants.CharacterType CharacterType { private set; get; }

        private ICharacterAnimation characterAnimation;

        public void Initialize(CharacterStatusSettings statusSettings, Constants.CharacterType characterType)
        {
            this.StatusController = statusSettings.Create();
            this.CharacterType = characterType;
            this.characterAnimation = this.GetComponentInChildren<ICharacterAnimation>();
            Assert.IsNotNull(this.characterAnimation);
        }

        public void StartAttack(Action animationCompleteAction)
        {
            this.characterAnimation.StartAttack(animationCompleteAction);
        }

        /// <summary>
        /// 攻撃力を加算する
        /// </summary>
        public void AddStrength(int value)
        {
            this.StatusController.Strength += value;
        }

        /// <summary>
        /// 防御力を加算する
        /// </summary>
        public void AddDefense(int value)
        {
            this.StatusController.Defense += value;
        }

        /// <summary>
        /// 思いやり力を加算する
        /// </summary>
        public void AddSympathy(int value)
        {
            this.StatusController.Sympathy += value;
        }

        /// <summary>
        /// ネガティブ力を加算する
        /// </summary>
        public void AddNega(int value)
        {
            this.StatusController.Nega += value;
        }

        /// <summary>
        /// 素早さを加算する
        /// </summary>
        public void AddSpeed(int value)
        {
            this.StatusController.Speed += value;
        }

        /// <summary>
        /// ダメージを受ける
        /// </summary>
        public void TakeDamage(int damage)
        {
            this.StatusController.HitPoint -= damage;
            Broker.Global.Publish(DamageNotify.Get(this, damage));

            if(this.StatusController.IsDead)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
