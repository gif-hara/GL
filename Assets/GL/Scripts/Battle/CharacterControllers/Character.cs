using System;
using GL.Battle.Commands.Bundle;
using GL.Battle.UI;
using GL.Database;
using GL.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace GL.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクター.
    /// </summary>
    public sealed class Character : MonoBehaviour
    {
        public CharacterRecord Record { get; private set; }
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

        public CharacterUIController UIController { get; private set; }

        //private ICharacterAnimation characterAnimation;

        public void Initialize(CharacterRecord characterRecord, Implement[] commands, AccessoryRecord[] accessories, int level, Constants.CharacterType characterType)
        {
            this.Record = characterRecord;
            this.StatusController = new CharacterStatusController(this, characterRecord, commands, level);
            this.AilmentController = new CharacterAilmentController(this);
            this.AccessoryController = new CharacterAccessoryController(accessories);
            this.CharacterType = characterType;
            // this.characterAnimation = this.GetComponentInChildren<ICharacterAnimation>();
            // Assert.IsNotNull(this.characterAnimation);

            this.UIController = this.GetComponent<CharacterUIController>();

            Broker.Global.Receive<StartBattle>()
                .Take(1)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.AccessoryController.OnStartBattle(_this);
                })
                .AddTo(this);


            Broker.Global.Receive<StartSelectCommand>()
                .Where(x => x.Character == this)
                .SubscribeWithState(this, (x, _this) =>
                {
                    this.StatusController.AddChargeTurn(1);

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
                        var command = BattleManager.Instance.ConfuseCommand;
                        command.Invoke(_this, command.GetTargets(x.Character));
                    }
                    else if (_this.AilmentController.Find(Constants.StatusAilmentType.Berserk))
                    {
                        Debug.Log("TODO: 狂暴を表現する");
                        var command = BattleManager.Instance.BerserkCommand;
                        command.Invoke(_this, command.GetTargets(x.Character));
                    }
                    else
                    {
                        if (_this.CharacterType == Constants.CharacterType.Enemy)
                        {
                            _this.InvokeCommandFromAI();
                        }
                    }

                    Broker.Global.Publish(VisibleRequestSelectCommandPanel.Get(this));
                })
                .AddTo(this);
        }
        
        public void StartAttack(Action onAttack, Action postprocess)
        {
            onAttack += postprocess;
            this.UIController.StartAttack(onAttack);
            // animationCompleteAction();
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
                    !this.AilmentController.Find(Constants.StatusAilmentType.Confuse) &&
                    !this.AilmentController.Find(Constants.StatusAilmentType.Berserk);
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
                //this.gameObject.SetActive(false);
                Broker.Global.Publish(DeadNotify.Get(this));
            }
        }

        /// <summary>
        /// 回復する
        /// </summary>
        public void Recovery(int amount)
        {
            var hp = this.StatusController.HitPoint;
            hp = Mathf.Min(hp + amount, this.StatusController.HitPointMax);
            this.StatusController.HitPoint = hp;
            Broker.Global.Publish(RecoveryNotify.Get(this, amount));
        }

        private void InternalEndTurn()
        {
            BattleManager.Instance.EndTurn(this);
        }

        private void InvokeCommandFromAI()
        {
            var animationController = this.UIController.AnimationController;
            if (!animationController.IsPlay)
            {
                this.StatusController.CharacterRecord.AIController.Invoke(this);
            }
            else
            {
                animationController.OnCompleteAsObservable()
                    .Take(1)
                    .SubscribeWithState(this, (_, _this) => _this.StatusController.CharacterRecord.AIController.Invoke(_this))
                    .AddTo(this);
            }
        }
    }
}
