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
            Broker.Global.Publish(DamageNotify.Get(this, damage));

            if(this.Status.IsDead)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
