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


            Broker.Global.Receive<NextTurn>()
                .Where(x => x.NextCharacter == this)
                .Where(_ => !this.CanMove)
                .SubscribeWithState(this, (_, _this) =>
                {
                    Debug.Log("TODO: 行動できなかった理由を表示");
                    _this.InternalEndTurn();
                })
                .AddTo(this);

            if (this.CharacterType == Constants.CharacterType.Enemy)
            {
                // FIXME: NextTurnじゃなくて敵AI経由でInvokeCommandを発行したい
                Broker.Global.Receive<NextTurn>()
                    .Where(x => x.NextCharacter == this)
                    .Where(_ => this.CanMove)
                    .SubscribeWithState(this, (_, _this) =>
                    {
                        var commands = _this.StatusController.Commands;
                        var command = commands[Random.Range(0, commands.Length)];
                        command.Invoke(_this);
                    })
                    .AddTo(this);
            }
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
                    !this.AilmentController.Find(Constants.StatusAilmentType.Paralysis) ||
                    !this.AilmentController.Find(Constants.StatusAilmentType.Sleep);
            }
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

        private void InternalEndTurn()
        {
            BattleManager.Instance.EndTurn(this);
        }
    }
}
