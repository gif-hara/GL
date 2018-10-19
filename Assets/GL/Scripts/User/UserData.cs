using System;
using System.Collections.Generic;
using System.Linq;
using GL.Systems;
using HK.Framework.Systems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.User
{
    /// <summary>
    /// ユーザーデータ
    /// </summary>
    [Serializable]
    public class UserData
    {
        public const int PartyCount = 3;
        
        private static UserData instance;
        public static UserData Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }

                instance = SaveData.GetClass<UserData>(SaveDataKey.UserData, null) ?? new UserData();
                if(instance != null)
                {
                    instance.currentPartyIndexReactiveProperty.Value = instance.currentPartyIndex;
                }

                return instance;
            }
        }

        [SerializeField]
        private Wallet wallet;
        public Wallet Wallet => this.wallet;

        [SerializeField]
        public InstanceData.Player Players = new InstanceData.Player();

        [SerializeField]
        public InstanceData.Party Parties = new InstanceData.Party();
        
        [SerializeField]
        public InstanceData.Weapon Weapons = new InstanceData.Weapon();

        [SerializeField]
        public InstanceData.Accessory Accessories = new InstanceData.Accessory();

        [SerializeField]
        private int currentPartyIndex = 0;

        private readonly ReactiveProperty<int> currentPartyIndexReactiveProperty = new ReactiveProperty<int>();
        public IReactiveProperty<int> CurrentPartyIndex { get { return this.currentPartyIndexReactiveProperty; } }

        public bool IsEmpty
        {
            get { return this.Parties.List.Count <= 0 && this.Players.List.Count <= 0; }
        }
        
        public void Initialize(UserData other)
        {
            Assert.AreEqual(this.Parties.List.Count + this.Players.List.Count, 0, "すでにユーザーデータが存在します");

            this.wallet = other.Wallet;
            this.Players.List.AddRange(other.Players.List.Select(p => p.Clone(this.Players.InstanceId)));
            this.Parties.List.AddRange(other.Parties.List.Select(p => p.Clone(this.Parties.InstanceId)));
            this.Weapons.List.AddRange(other.Weapons.List.Select(w => w.Clone(this.Weapons.InstanceId)));
            this.Accessories.List.AddRange(other.Accessories.List.Select(a => a.Clone(this.Accessories.InstanceId)));
            this.currentPartyIndexReactiveProperty.Value = other.currentPartyIndexReactiveProperty.Value;
        }

        public void Save()
        {
            SaveData.SetClass(SaveDataKey.UserData, this);
            SaveData.Save();
        }

        public void SetCurrentPartyIndex(int index)
        {
            this.currentPartyIndex = index;
            this.currentPartyIndexReactiveProperty.Value = index;
            this.Save();
        }

        public Party CurrentParty
        {
            get { return this.Parties.List[this.currentPartyIndex]; }
        }

        /// <summary>
        /// 武器を追加する
        /// </summary>
        /// <remarks>
        /// セーブはしていないので個別でセーブしてください
        /// </remarks>
        public void AddWeapon(Battle.Weapon weapon)
        {
            this.Weapons.List.Add(new User.Weapon(this.Weapons.InstanceId, weapon.Id));
        }

        /// <summary>
        /// <paramref name="weapon"/>は誰かが装備しているか返す
        /// </summary>
        public bool IsEquipedWeapon(User.Weapon weapon) => this.Players.List.FindIndex(p => p.IsEquipedWeapon(weapon.InstanceId)) >= 0;
    }
}
