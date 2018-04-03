using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Scripts.Battle.CharacterControllers
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public sealed class CharacterAnimation : MonoBehaviour, ICharacterAnimation
    {
        private Animator animator;

        public ObservableStateMachineTrigger StateMachineTrigger{ private set; get; }

        // private 

        public static class AnimatorParameter
        {
            public static int MoveSpeed = Animator.StringToHash("MoveSpeed");
            public static int Attack = Animator.StringToHash("Attack");
            public static int Dead = Animator.StringToHash("Dead");
        }

        void Awake()
        {
            this.animator = this.GetComponent<Animator>();
            Assert.IsNotNull(this.animator);

            this.StateMachineTrigger = this.animator.GetBehaviour<ObservableStateMachineTrigger>();
            Assert.IsNotNull(this.StateMachineTrigger);
        }

        void ICharacterAnimation.StartAttack(Action stateExitAction)
        {
            this.animator.SetTrigger(AnimatorParameter.Attack);
            this.StateMachineTrigger.OnStateExitAsObservable()
                .First(s => this.IsMatchState(s, AnimatorParameter.Attack))
                .SubscribeWithState(stateExitAction, (_, action) => action())
                .AddTo(this);
        }

        void ICharacterAnimation.StartDead(Action stateExitAction)
        {
            this.animator.SetTrigger(AnimatorParameter.Dead);
            this.StateMachineTrigger.OnStateExitAsObservable()
                .First(s => this.IsMatchState(s, AnimatorParameter.Dead))
                .SubscribeWithState(stateExitAction, (_, action) => action())
                .AddTo(this);
        }

        private bool IsMatchState(ObservableStateMachineTrigger.OnStateInfo stateInfo, int hash)
        {
            return stateInfo.StateInfo.shortNameHash == hash;
        }
    }
}
