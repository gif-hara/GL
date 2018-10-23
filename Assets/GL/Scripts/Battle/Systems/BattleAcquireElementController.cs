using System.Collections.Generic;
using System.Linq;
using GL.Events.Battle;
using GL.User;
using HK.Framework.EventSystems;
using HK.GL.Extensions;
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

        public Dictionary<Materials.Material, int> Materials { get; private set; } = new Dictionary<Materials.Material, int>();

        public UnlockElements UnlockElements { get; private set; }

        public BattleAcquireElementController(GameObject owner)
        {
            Broker.Global.Receive<DeadNotify>()
                .Where(x => x.Character.CharacterType == Constants.CharacterType.Enemy)
                .SubscribeWithState(this, (x, _this) =>
                {
                    var b = x.Character.StatusController.CharacterRecord;
                    _this.Experience += b.AcquireExperience;
                    _this.Gold += b.Price;
                    b.MaterialLotteries
                        .Where(m => m.IsAcquire)
                        .Select(m => m.Material)
                        .ForEach(m =>
                        {
                            if (this.Materials.ContainsKey(m))
                            {
                                this.Materials[m]++;
                            }
                            else
                            {
                                this.Materials.Add(m, 1);
                            }
                        });
                })
                .AddTo(owner);
            Broker.Global.Receive<EndBattle>()
                .Where(x => x.Result == Constants.BattleResult.PlayerWin)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.UnlockElements = UserData.Instance.UnlockElements.GetNotDuplicate(BattleManager.Instance.Parties.Enemy.PartyRecord.UnlockElements);
                    _this.ApplyUserData();
                })
                .AddTo(owner);
        }

        private void ApplyUserData()
        {
            var u = UserData.Instance;
            u.Wallet.Experience.Add(this.Experience);
            u.Wallet.Gold.Add(this.Gold);
            this.Materials.ForEach(m => u.AddMaterial(m.Key, m.Value));
            u.AddUnlockElements(this.UnlockElements);
            u.Save();
        }
    }
}
