using System;
using GL.Events.Battle;
using GL.Database;
using GL.UI.PopupControllers;
using HK.Framework.EventSystems;
using HK.GL.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle
{
    /// <summary>
    /// バトル終了処理を制御するクラス
    /// </summary>
    public sealed class EndBattleController : MonoBehaviour
    {
        [SerializeField]
        private ResultWinPopupController resultWinPopupController;

        [SerializeField]
        private SimplePopupStrings unlockEnemyPartyPopup;

        [SerializeField]
        private SimplePopupStrings unlockCharacterPopup;

        [SerializeField]
        private SimplePopupStrings unlockWeaponPopup;

        [SerializeField]
        private SimplePopupStrings unlockAccessoryPopup;

        void Awake()
        {
            Broker.Global.Receive<EndBattle>()
                .Take(1)
                .Subscribe(x =>
                {
                    Observable.Timer(TimeSpan.FromSeconds(0.5f))
                        .SubscribeWithState(this, (_, _this) =>
                        {
                            _this.CreateResultWinPopup()
                                .SelectMany(__ => this.CreateUnlockEnemyPartyPopup())
                                .SelectMany(__ => this.CreateUnlockCharacterPopup())
                                .SelectMany(__ => this.CreateUnlockWeaponPopup())
                                .SelectMany(__ => this.CreateUnlockAccessoryPopup())
                                .Subscribe(__ => BattleManager.Instance.ToHomeScene())
                                .AddTo(_this);
                        })
                        .AddTo(this);
                })
                .AddTo(this);
        }

        private IObservable<int> CreateResultWinPopup()
        {
            var acquireElement = BattleManager.Instance.AcquireElementController;
            return PopupManager
                .Show(this.resultWinPopupController)
                .Setup(acquireElement.Experience, acquireElement.Gold, acquireElement.Materials)
                .CloseOnSubmit()
                .SubmitAsObservable();
        }

        private IObservable<Unit> CreateUnlockEnemyPartyPopup()
        {
            return Observable.Create<Unit>(observer => this.CreateUnlockEnemyPartyPopup(0, observer));
        }

        private IDisposable CreateUnlockEnemyPartyPopup(int index, IObserver<Unit> completeStream)
        {
            var enemyParties = BattleManager.Instance.AcquireElementController.UnlockElements.EnemyParties;
            return this.CreateNotifyPopup(
                index,
                completeStream,
                i => enemyParties.Count <= i,
                () => this.unlockEnemyPartyPopup.Show(null, f => f.Format(MasterData.EnemyParty.GetById(enemyParties[index]).PartyName), null),
                (i, c) => this.CreateUnlockEnemyPartyPopup(i, c)
            );
        }

        private IObservable<Unit> CreateUnlockCharacterPopup()
        {
            return Observable.Create<Unit>(observer => this.CreateUnlockCharacterPopup(0, observer));
        }

        private IDisposable CreateUnlockCharacterPopup(int index, IObserver<Unit> completeStream)
        {
            var characters = BattleManager.Instance.AcquireElementController.UnlockElements.Characters;
            return this.CreateNotifyPopup(
                index,
                completeStream,
                i => characters.Count <= i,
                () => this.unlockCharacterPopup.Show(null, f => f.Format(MasterData.Character.GetById(characters[index]).CharacterName), null),
                (i, c) => this.CreateUnlockCharacterPopup(i, c)
            );
        }

        private IObservable<Unit> CreateUnlockWeaponPopup()
        {
            return Observable.Create<Unit>(observer => this.CreateUnlockWeaponPopup(0, observer));
        }

        private IDisposable CreateUnlockWeaponPopup(int index, IObserver<Unit> completeStream)
        {
            var weapons = BattleManager.Instance.AcquireElementController.UnlockElements.Weapons;
            return this.CreateNotifyPopup(
                index,
                completeStream,
                i => weapons.Count <= i,
                () => this.unlockWeaponPopup.Show(null, f => f.Format(MasterData.Equipment.GetById(weapons[index]).EquipmentName), null),
                (i, c) => this.CreateUnlockWeaponPopup(i, c)
            );
        }

        private IObservable<Unit> CreateUnlockAccessoryPopup()
        {
            return Observable.Create<Unit>(observer => this.CreateUnlockAccessoryPopup(0, observer));
        }

        private IDisposable CreateUnlockAccessoryPopup(int index, IObserver<Unit> completeStream)
        {
            var accessories = BattleManager.Instance.AcquireElementController.UnlockElements.Accessories;
            return this.CreateNotifyPopup(
                index,
                completeStream,
                i => accessories.Count <= i,
                () => this.unlockWeaponPopup.Show(null, f => f.Format(MasterData.Accessory.GetById(accessories[index]).AccessoryName), null),
                (i, c) => this.CreateUnlockAccessoryPopup(i, c)
            );
        }

        private IDisposable CreateNotifyPopup(int index, IObserver<Unit> completeStream, Func<int, bool> isCompleteSelector, Func<PopupBase> popupSelector, Action<int, IObserver<Unit>> submitAction)
        {
            var acquireElement = BattleManager.Instance.AcquireElementController;

            // 全て表示した場合は完了したことを通知する
            if (isCompleteSelector(index))
            {
                completeStream.OnNext(Unit.Default);
                completeStream.OnCompleted();
                return Disposable.Empty;
            }
            else
            {
                return popupSelector()
                    .CloseOnSubmit()
                    .SubmitAsObservable()
                    .SubscribeWithState3(submitAction, index, completeStream, (_, s, i, c) =>
                    {
                        s(i + 1, c);
                    })
                    .AddTo(this);
            }
        }
    }
}
