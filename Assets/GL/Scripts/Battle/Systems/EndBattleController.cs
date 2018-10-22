using System;
using GL.Events.Battle;
using GL.MasterData;
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
                () => this.unlockEnemyPartyPopup.Show(null, f => f.Format(Database.EnemyParty.List.Find(e => e.Id == enemyParties[index]).PartyName), null),
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
                () => this.unlockCharacterPopup.Show(null, f => f.Format(Database.Character.List.Find(c => c.Id == characters[index]).CharacterName), null),
                (i, c) => this.CreateUnlockCharacterPopup(i, c)
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
