using System.Collections.Generic;
using GL.Scripts.Systems;
using HK.Framework.Systems;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Scripts.User
{
    /// <summary>
    /// ユーザーデータ
    /// </summary>
    [SerializeField]
    public class UserData
    {        
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

        public bool IsEmpty
        {
            get { return this.Parties.Count <= 0 && this.Players.Count <= 0; }
        }
        
        public void Initialize(Party initialParty, IEnumerable<Player> initialPlayers)
        {
            Assert.AreEqual(this.Parties.Count + this.Players.Count, 0, "すでにユーザーデータが存在します");
            this.Parties.Add(initialParty);
            
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
            
            return instance = SaveData.GetClass<UserData>(SaveDataKey.UserData, null) ?? new UserData();
        }
    }
}
