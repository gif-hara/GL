using System.Collections.Generic;
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
    [SerializeField]
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
        public List<Party> Parties = new List<Party>();
        
        [SerializeField]
        public List<Player> Players = new List<Player>();

        [SerializeField]
        private int currentPartyIndex = 0;

        private readonly ReactiveProperty<int> currentPartyIndexReactiveProperty = new ReactiveProperty<int>();
        public IReactiveProperty<int> CurrentPartyIndex { get { return this.currentPartyIndexReactiveProperty; } }

        public bool IsEmpty
        {
            get { return this.Parties.Count <= 0 && this.Players.Count <= 0; }
        }
        
        public void Initialize(Party initialParty, IEnumerable<Player> initialPlayers)
        {
            Assert.AreEqual(this.Parties.Count + this.Players.Count, 0, "すでにユーザーデータが存在します");

            for (var i = 0; i < PartyCount; i++)
            {
                this.Parties.Add(initialParty);
            }
            
            initialParty.Players.ForEach(p => this.Players.Add(p));
            this.Players.AddRange(initialPlayers);
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
