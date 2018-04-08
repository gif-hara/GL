using System;
using GL.Scripts.Battle.Systems;
using GL.Scripts.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

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
        
        public CharacterAccessoryController AccessoryController { get; private set; }

        public Constants.CharacterType CharacterType { get; private set; }

        private ICharacterAnimation characterAnimation;

        public void Initialize(Blueprint blueprint, Constants.CharacterType characterType)
        {
            this.StatusController = new CharacterStatusController(blueprint);
            this.AilmentController = new CharacterAilmentController(this);
            this.AccessoryController = new CharacterAccessoryController(blueprint);
            this.CharacterType = characterType;
            this.characterAnimation = this.GetComponentInChildren<ICharacterAnimation>();
            Assert.IsNotNull(this.characterAnimation);

            Broker.Global.Receive<StartBattle>()
                .Take(1)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.AccessoryController.OnStartBattle(_this);
                })
                .AddTo(this);


            Broker.Global.Receive<SelectCommand>()
                .Where(x => x.Character == this)
                .SubscribeWithState(this, (_, _this) =>
                {
                    if (_this.AilmentController.Find(Constants.StatusAilmentType.Paralysis))
                    {
                        Debug.Log("TODO: 麻痺を表現する");
                        _this.InternalEndTurn();
                    }
                    else if (_this.AilmentController.Find(Constants.StatusAilmentType.Sleep))
                    {
                        Debug.Log("TODO: 睡眠を表現する");
                        _this.InternalEndTurn();
                    }
                    else if (_this.AilmentController.Find(Constants.StatusAilmentType.Confuse))
                    {
                        Debug.Log("TODO: 混乱を表現する");
                        BattleManager.Instance.ConfuseCommand.Invoke(_this);
                    }
                    else
                    {
                        if (_this.CharacterType == Constants.CharacterType.Enemy)
                        {
                            var commands = _this.StatusController.Commands;
                            var command = commands[Random.Range(0, commands.Length)];
                            command.Invoke(_this);
                        }
                    }
                })
                .AddTo(this);
        }
        
        public void StartAttack(Action animationCompleteAction)
        {
            animationCompleteAction += this.InternalEndTurn;
            this.characterAnimation.StartAttack(animationCompleteAction);
        }

        /// <summary>
        /// 次のターンに行動する際に行動可能か返す
        /// </summary>
        public bool CanMove
        {
            get
            {
                return
                    !this.AilmentController.Find(Constants.StatusAilmentType.Paralysis) &&
                    !this.AilmentController.Find(Constants.StatusAilmentType.Sleep) &&
                    !this.AilmentController.Find(Constants.StatusAilmentType.Confuse);
            }
        }

        /// <summary>
        /// ダメージを受ける
        /// </summary>
        public void TakeDamage(int damage)
        {
            this.AilmentController.TakeDamage();
            this.StatusController.HitPoint -= damage;
            Broker.Global.Publish(DamageNotify.Get(this, damage));

            if(this.StatusController.IsDead)
            {
                this.gameObject.SetActive(false);
            }
        }

        private void InternalEndTurn()
        {
            BattleManager.Instance.EndTurn(this);
        }
    }
}
