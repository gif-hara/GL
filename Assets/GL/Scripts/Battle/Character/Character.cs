using UnityEngine;
using UnityEngine.Assertions;
using System;
using HK.Framework.EventSystems;
using HK.GL.Events.Battle;

namespace HK.GL.Battle
{
    /// <summary>
    /// キャラクター.
    /// </summary>
    public sealed class Character : MonoBehaviour
    {
        public CharacterStatus Status { private set; get; }

        public Constants.CharacterType CharacterType { private set; get; }

        private ICharacterAnimation characterAnimation;

        public void Initialize(CharacterStatusSettings statusSettings, Constants.CharacterType characterType)
        {
            this.Status = statusSettings.Create();
            this.CharacterType = characterType;
            this.characterAnimation = this.GetComponentInChildren<ICharacterAnimation>();
            Assert.IsNotNull(this.characterAnimation);
        }

        public void StartAttack(Action animationCompleteAction)
        {
            this.characterAnimation.StartAttack(animationCompleteAction);
        }

        /// <summary>
        /// 防御力上昇を行う
        /// </summary>
        public void AddDefense(int value)
        {
            this.Status.AddDefense(value);
        }

        /// <summary>
        /// ダメージを受ける
        /// </summary>
        public void TakeDamage(int damage)
        {
            this.Status.TakeDamage(damage);
            UniRxEvent.GlobalBroker.Publish(DamageNotify.Get(this, damage));

            if(this.Status.IsDead)
            {
                this.characterAnimation.StartDead(() => this.gameObject.SetActive(false));
            }
        }
    }
}
