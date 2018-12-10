using System.Collections.Generic;
using System.Linq;
using GL.Database;
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

        public Dictionary<MaterialRecord, int> Materials { get; private set; } = new Dictionary<MaterialRecord, int>();

        public UnlockElements UnlockElements { get; private set; }

        public BattleAcquireElementController(GameObject owner)
        {
            Broker.Global.Receive<DeadNotify>()
                .Where(x => x.Character.CharacterType == Constants.CharacterType.Enemy)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.AddMaterial(x.Character.StatusController.CharacterRecord);
                })
                .AddTo(owner);
            Broker.Global.Receive<EndBattle>()
                .Where(x => x.Result == Constants.BattleResult.PlayerWin)
                .SubscribeWithState(this, (_, _this) =>
                {
                    var unlockEquipments = BattleManager.Instance.Parties.Enemy.PartyRecord.UnlockElements;
                    unlockEquipments.Equipments.AddRange(_this.GetUnlockEquipments());
                    _this.UnlockElements = UserData.Instance.UnlockElements.GetNotDuplicate(unlockEquipments);
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

        private void AddMaterial(CharacterRecord characterRecord)
        {
            this.Experience += characterRecord.AcquireExperience;
            this.Gold += characterRecord.Price;
            characterRecord.MaterialLotteries
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
        }

        private List<string> GetUnlockEquipments()
        {
            var result = new List<string>();
            var equipmentList = MasterData.Equipment;
            this.Materials.ForEach(m =>
            {
                var unlockEquipments = equipmentList.List.FindAll(e =>
                {
                    if(e.NeedMaterials.Length <= 0)
                    {
                        return false;
                    }
                    return e.NeedMaterials[0].Material == m.Key;
                })
                .Select(e => e.Id);
                result.AddRange(unlockEquipments);
            });

            return result;
        }
    }
}
