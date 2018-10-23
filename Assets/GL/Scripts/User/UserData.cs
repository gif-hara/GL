using System;
using System.Collections.Generic;
using System.Linq;
using GL.Database;
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
        public const int PartyCount = 4;
        
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
        private string userName;
        public string UserName => this.userName;

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
        public List<Material> Materials = new List<Material>();

        [SerializeField]
        private UnlockElements unlockElements = new UnlockElements();
        public UnlockElements UnlockElements => this.unlockElements;

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

            this.userName = other.userName;
            this.wallet = other.Wallet;
            this.Players.List.AddRange(other.Players.List.Select(p => p.Clone(this.Players.InstanceId)));
            this.Parties.List.AddRange(other.Parties.List.Select(p => p.Clone(this.Parties.InstanceId)));
            this.Weapons.List.AddRange(other.Weapons.List.Select(w => w.Clone(this.Weapons.InstanceId)));
            this.Accessories.List.AddRange(other.Accessories.List.Select(a => a.Clone(this.Accessories.InstanceId)));
            this.Materials = other.Materials;
            this.unlockElements = other.unlockElements;
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
        public void AddWeapon(WeaponRecord weapon)
        {
            this.Weapons.List.Add(new User.Weapon(this.Weapons.InstanceId, weapon.Id));
        }

        /// <summary>
        /// <paramref name="weapon"/>は誰かが装備しているか返す
        /// </summary>
        public bool IsEquipedWeapon(User.Weapon weapon) => this.Players.List.FindIndex(p => p.IsEquipedWeapon(weapon.InstanceId)) >= 0;

        /// <summary>
        /// プレイヤーを追加する
        /// </summary>
        public void AddPlayer(CharacterRecord blueprint)
        {
            this.Players.List.Add(Player.Create(this.Players.InstanceId, 1, blueprint.Id, 0, 0));
        }

        /// <summary>
        /// 素材を取得する　
        /// </summary>
        public void AddMaterial(Materials.Material material, int value = 1)
        {
            var m = this.Materials.Find(x => x.Id == material.Id);
            if(m == null)
            {
                m = new Material(material.Id);
                this.Materials.Add(m);
            }

            m.Count += value;
        }

        /// <summary>
        /// アンロック要素を追加する
        /// </summary>
        public void AddUnlockElements(UnlockElements elements)
        {
            elements.EnemyParties.ForEach(e =>
            {
                Assert.IsTrue(this.unlockElements.EnemyParties.FindIndex(x => e == x) < 0, $"{0}の敵パーティはすでにアンロックしています");
                this.unlockElements.EnemyParties.Add(e);
            });
            elements.Characters.ForEach(c =>
            {
                Assert.IsTrue(this.unlockElements.Characters.FindIndex(x => c == x) < 0, $"{0}のキャラクターはすでにアンロックしています");
                this.unlockElements.Characters.Add(c);
            });
            elements.Weapons.ForEach(w =>
            {
                Assert.IsTrue(this.unlockElements.Weapons.FindIndex(x => w == x) < 0, $"{0}の武器はすでにアンロックしています");
                this.unlockElements.Weapons.Add(w);
            });
        }
    }
}
