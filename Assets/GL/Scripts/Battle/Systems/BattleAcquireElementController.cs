using GL.Events.Battle;
using GL.User;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle
{
    /// <summary>
    /// バトルで獲得した要素を制御するクラス
    /// </summary>
    public sealed class BattleAcquireElementController
    {
        public int Experience { get; private set; }

        public int Gold { get; private set; }

        public BattleAcquireElementController(GameObject owner)
        {
            Broker.Global.Receive<DeadNotify>()
                .Where(x => x.Character.CharacterType == Constants.CharacterType.Enemy)
                .SubscribeWithState(this, (x, _this) =>
                {
                    var b = x.Character.StatusController.Blueprint;
                    _this.Experience = b.AcquireExperience;
                    _this.Gold = b.Price;
                })
                .AddTo(owner);
            Broker.Global.Receive<EndBattle>()
                .Where(x => x.Result == Constants.BattleResult.PlayerWin)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.ApplyUserData();
                })
                .AddTo(owner);
        }

        private void ApplyUserData()
        {
            var u = UserData.Instance;
            u.Wallet.Experience.Add(this.Experience);
            u.Wallet.Gold.Add(this.Gold);
            u.Save();
        }
    }
}
