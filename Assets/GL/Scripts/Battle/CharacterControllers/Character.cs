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
        /// ステータスコントローラー
        /// </summary>
        public CharacterStatusController StatusController { get; private set; }

        /// <summary>
        /// 状態異常コントローラー
        /// </summary>
        public CharacterAilmentController AilmentController { get; private set; }

        public Constants.CharacterType CharacterType { get; private set; }

        private ICharacterAnimation characterAnimation;

        public void Initialize(Blueprint blueprint, Constants.CharacterType characterType)
        {
            this.StatusController = new CharacterStatusController(blueprint);
            this.AilmentController = new CharacterAilmentController(this);
            this.CharacterType = characterType;
            this.characterAnimation = this.GetComponentInChildren<ICharacterAnimation>();
            Assert.IsNotNull(this.characterAnimation);
        }

        public void StartAttack(Action animationCompleteAction)
        {
            animationCompleteAction += () => BattleManager.Instance.EndTurn(this);
            this.characterAnimation.StartAttack(animationCompleteAction);
        }

        /// <summary>
        /// 攻撃力を加算する
        /// </summary>
        public void AddStrength(int value)
        {
            this.StatusController.AdditiveStatus.Strength += value;
        }

        /// <summary>
        /// 防御力を加算する
        /// </summary>
        public void AddDefense(int value)
        {
            this.StatusController.AdditiveStatus.Defense += value;
        }

        /// <summary>
        /// 思いやり力を加算する
        /// </summary>
        public void AddSympathy(int value)
        {
            this.StatusController.AdditiveStatus.Sympathy += value;
        }

        /// <summary>
        /// ネガティブ力を加算する
        /// </summary>
        public void AddNega(int value)
        {
            this.StatusController.AdditiveStatus.Nega += value;
        }

        /// <summary>
        /// 素早さを加算する
        /// </summary>
        public void AddSpeed(int value)
        {
            this.StatusController.AdditiveStatus.Speed += value;
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
