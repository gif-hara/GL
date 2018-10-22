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
            var acquireElement = BattleManager.Instance.AcquireElementController;

            // 全て表示した場合は完了したことを通知する
            if(acquireElement.UnlockElements.EnemyParties.Count <= index)
            {
                completeStream.OnNext(Unit.Default);
                completeStream.OnCompleted();
                return Disposable.Empty;
            }
            else
            {
                var blueprint = Database.EnemyParty.List.Find(e => e.Id == acquireElement.UnlockElements.EnemyParties[index]);
                return this.unlockEnemyPartyPopup.Show(null, f => f.Format(blueprint.PartyName), null)
                    .CloseOnSubmit()
                    .SubmitAsObservable()
                    .SubscribeWithState3(this, index, completeStream, (_, _this, i, c) =>
                    {
                        _this.CreateUnlockEnemyPartyPopup(i + 1, c);
                    })
                    .AddTo(this);
            }
        }
    }
}
