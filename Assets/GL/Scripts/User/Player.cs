using System;
using System.Collections.Generic;
using System.Linq;
using GL.Battle.CharacterControllers;
using GL.MasterData;
using HK.GL.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.User
{
    /// <summary>
    /// ユーザーデータのプレイヤークラス
    /// </summary>
    [Serializable]
    public sealed class Player : IInstanceId
    {
        [SerializeField][HideInInspector]
        private int instanceId;
        public int InstanceId => this.instanceId;

        [SerializeField]
        public string PlayerName;
        
        [SerializeField][Range(1.0f, 100.0f)]
        public int Level = 1;
        
        [SerializeField]
        public string BlueprintId;

        [SerializeField]
        public int RightWeaponInstanceId;

        [SerializeField]
        public int LeftWeaponInstanceId;

        [SerializeField]
        public List<int> AccessoryInstanceIds = new List<int>();

        public Parameter Parameter { get { return this.Blueprint.GetParameter(this.Level); } }

        public Resistance Resistance { get { return this.Blueprint.Resistance; } }

        private Blueprint cachedBlueprint = null;

        private Player()
        {
        }

        public static Player Create(InstanceId instanceId, int level, string blueprintId, int rightWeaponInstanceId, int leftWeaponInstanceId)
        {
            return new Player()
            {
                instanceId = instanceId.Issue,
                PlayerName = "",
                Level = level,
                BlueprintId = blueprintId,
                RightWeaponInstanceId = rightWeaponInstanceId,
                LeftWeaponInstanceId = leftWeaponInstanceId
            };
        }

        /// <summary>
        /// 自分自身のクローンを返す
        /// </summary>
        public Player Clone(InstanceId instanceId)
        {
            return new Player()
            {
                instanceId = instanceId.Issue,
                PlayerName = this.PlayerName,
                Level = this.Level,
                BlueprintId = this.BlueprintId,
                RightWeaponInstanceId = this.RightWeaponInstanceId,
                LeftWeaponInstanceId = this.LeftWeaponInstanceId
            };
        }

        public bool IsPossessionRightWeapon => this.RightWeaponInstanceId != 0;

        public User.Weapon RightUserWeapon => UserData.Instance.Weapons.GetByInstanceId(this.RightWeaponInstanceId);

        public Battle.Weapon RightBattleWeapon => this.IsPossessionRightWeapon ? this.RightUserWeapon.BattleWeapon : null;

        public bool IsPossessionLeftWeapon => this.LeftWeaponInstanceId != 0;

        public User.Weapon LeftUserWeapon => UserData.Instance.Weapons.GetByInstanceId(this.LeftWeaponInstanceId);

        public Battle.Weapon LeftBattleWeapon => this.IsPossessionLeftWeapon ? this.LeftUserWeapon.BattleWeapon : null;

        /// <summary>
        /// <paramref name="instanceId"/>の武器を装備しているか返す
        /// </summary>
        public bool IsEquipedWeapon(int instanceId) => this.RightWeaponInstanceId == instanceId || this.LeftWeaponInstanceId == instanceId;

        /// <summary>
        /// 武器を切り替える
        /// </summary>
        public void ChangeWeapon(Constants.HandType handType, int instanceId)
        {
            if(handType == Constants.HandType.Right)
            {
                this.RightWeaponInstanceId = instanceId;
            }
            else
            {
                this.LeftWeaponInstanceId = instanceId;
            }
        }

        public void LevelUp()
        {
            this.Level++;
        }

        public bool IsLevelMax => this.Level >= Constants.LevelMax;

        public Battle.CharacterControllers.Blueprint Blueprint
        {
            get
            {
                this.cachedBlueprint = this.cachedBlueprint ?? Database.Character.List.Find(b => b.Id == this.BlueprintId);
                Assert.IsNotNull(this.cachedBlueprint, $"Id = {this.BlueprintId}の{typeof(Battle.CharacterControllers.Blueprint).Name}が存在しません");

                return this.cachedBlueprint;
            }
        }

        public Battle.Accessory[] Accessories
        {
            get
            {
                var userData = UserData.Instance;
                return this.AccessoryInstanceIds.Select(instanceId => Database.Accessory.List.Find(a => a.Id == userData.Accessories.GetByInstanceId(instanceId).Id)).ToArray();
            }
        }

        /// <summary>
        /// バトルで利用するコマンドを返す
        /// </summary>
        public Battle.Commands.Bundle.Blueprint[] UsingCommands
        {
            get
            {
                var result = new List<Battle.Commands.Bundle.Blueprint>();
                if(IsPossessionRightWeapon)
                {
                    result.AddRange(this.RightBattleWeapon.Commands);
                }
                if(IsPossessionLeftWeapon)
                {
                    result.AddRange(this.LeftBattleWeapon.Commands);
                }

                return result.ToArray();
            }
        }
    }
}
