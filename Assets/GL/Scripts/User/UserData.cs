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
                return Load();
            }
        }

        [SerializeField]
        public List<Player> Players = new List<Player>();

        [SerializeField]
        public List<Party> Parties = new List<Party>();
        
        [SerializeField]
        public List<Weapon> Weapons = new List<Weapon>();

        [SerializeField]
        private int currentPartyIndex = 0;

        private readonly ReactiveProperty<int> currentPartyIndexReactiveProperty = new ReactiveProperty<int>();
        public IReactiveProperty<int> CurrentPartyIndex { get { return this.currentPartyIndexReactiveProperty; } }

        public bool IsEmpty
        {
            get { return this.Parties.Count <= 0 && this.Players.Count <= 0; }
        }
        
        public void Initialize(UserData other)
        {
            Assert.AreEqual(this.Parties.Count + this.Players.Count, 0, "すでにユーザーデータが存在します");

            this.Parties.AddRange(other.Parties.Select(p => p.Clone));
            this.Parties.ForEach(party =>
            {
                party.Players.ForEach(player => this.Players.Add(player));
            });
            this.Players.AddRange(other.Players.Select(p => p.Clone));
            this.Weapons.AddRange(other.Weapons);
        }

        public void Save()
        {
            SaveData.SetClass(SaveDataKey.UserData, this);
        }

        public static UserData Load()
        {
            if (instance != null)
            {
                return instance;
            }
            
            instance = SaveData.GetClass<UserData>(SaveDataKey.UserData, null) ?? new UserData();
            instance.currentPartyIndexReactiveProperty.Value = instance.currentPartyIndex;

            return instance;
        }

        public void SetCurrentPartyIndex(int index)
        {
            this.currentPartyIndex = index;
            this.currentPartyIndexReactiveProperty.Value = index;
            this.Save();
        }

        public Party CurrentParty
        {
            get { return this.Parties[this.currentPartyIndex]; }
        }
    }
}
