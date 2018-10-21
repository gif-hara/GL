using System;
using GL.Events.Battle;
using GL.UI.PopupControllers;
using HK.Framework.EventSystems;
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
        private BattleManager battleManager;

        [SerializeField]
        private ResultWinPopupController resultWinPopupController;

        void Awake()
        {
            Broker.Global.Receive<EndBattle>()
                .Take(1)
                .Subscribe(x =>
                {
                    Observable.Timer(TimeSpan.FromSeconds(0.2f))
                        .SubscribeWithState(this, (_, _this) =>
                        {
                            _this.CreateResultWinPopup();
                        })
                        .AddTo(this);
                })
                .AddTo(this);
        }

        private void CreateResultWinPopup()
        {
            var acquireElement = this.battleManager.AcquireElementController;
            var popup = PopupManager.Show(this.resultWinPopupController).Setup(acquireElement.Experience, acquireElement.Gold, acquireElement.Materials);
            popup.SubmitAsObservable()
                .Select(x => (ResultWinPopupController.SubmitType)x)
                .SubscribeWithState2(this, popup, (x, _this, _popup) =>
                {
                    switch (x)
                    {
                        case ResultWinPopupController.SubmitType.ToHome:
                            PopupManager.Close(_popup);
                            _this.battleManager.ToHomeScene();
                            break;
                        default:
                            Assert.IsTrue(false, $"{x}は未対応の値です");
                            break;
                    }
                })
                .AddTo(this);
        }
    }
}
