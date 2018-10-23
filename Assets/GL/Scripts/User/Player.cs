using System;
using System.Collections.Generic;
using System.Linq;
using GL.Battle.CharacterControllers;
using GL.Database;
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
        private Hand rightHand;
        public Hand RightHand => this.rightHand;

        [SerializeField]
        private Hand leftHand;
        public Hand LeftHand => this.leftHand;

        [SerializeField]
        public List<int> AccessoryInstanceIds = new List<int>();

        public Parameter Parameter { get { return this.CharacterRecord.GetParameter(this.Level); } }

        public Resistance Resistance { get { return this.CharacterRecord.Resistance; } }

        private CharacterRecord cachedBlueprint = null;

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
                rightHand = new Hand(rightWeaponInstanceId),
                leftHand = new Hand(leftWeaponInstanceId)
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
                rightHand = this.rightHand,
                leftHand = this.leftHand
            };
        }

        /// <summary>
        /// <paramref name="instanceId"/>の武器を装備しているか返す
        /// </summary>
        public bool IsEquipedWeapon(int instanceId) => this.RightHand.WeaponInstanceId == instanceId || this.LeftHand.WeaponInstanceId == instanceId;

        /// <summary>
        /// <paramref name="weaponId"/>と同じ武器を既に装備しているか返す
        /// </summary>
        public bool IsEquipedSameWeapon(string weaponId) => (this.RightHand.IsPossession && this.RightHand.UserWeapon.Id == weaponId) || (this.LeftHand.IsPossession && this.LeftHand.UserWeapon.Id == weaponId);

        /// <summary>
        /// 武器を切り替える
        /// </summary>
        public void ChangeWeapon(Constants.HandType handType, int instanceId)
        {
            if(handType == Constants.HandType.Right)
            {
                this.RightHand.WeaponInstanceId = instanceId;
            }
            else
            {
                this.LeftHand.WeaponInstanceId = instanceId;
            }
        }

        public void LevelUp()
        {
            this.Level++;
        }

        public bool IsLevelMax => this.Level >= Constants.LevelMax;

        public CharacterRecord CharacterRecord
        {
            get
            {
                this.cachedBlueprint = this.cachedBlueprint ?? MasterData.Character.GetById(this.BlueprintId);
                Assert.IsNotNull(this.cachedBlueprint, $"Id = {this.BlueprintId}の{typeof(CharacterRecord).Name}が存在しません");

                return this.cachedBlueprint;
            }
        }

        public AccessoryRecord[] Accessories
        {
            get
            {
                var userData = UserData.Instance;
                return this.AccessoryInstanceIds.Select(instanceId => MasterData.Accessory.GetById(userData.Accessories.GetByInstanceId(instanceId).Id)).ToArray();
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
                if(this.RightHand.IsPossession)
                {
                    result.AddRange(this.RightHand.BattleWeapon.Commands);
                }
                if(this.LeftHand.IsPossession)
                {
                    result.AddRange(this.LeftHand.BattleWeapon.Commands);
                }

                return result.ToArray();
            }
        }
    }
}
