using System;
using System.Collections.Generic;
using System.Linq;
using GL.Battle;
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
        private int[] accessoryInstanceIds = new int[0];
        public int[] AccessoryInstanceIds => this.accessoryInstanceIds;

        public Parameter Parameter { get { return this.CharacterRecord.GetParameter(this.Level); } }

        public Resistance Resistance { get { return this.CharacterRecord.Resistance; } }

        private CharacterRecord cachedBlueprint = null;

        private Player()
        {
        }

        public static Player Create(InstanceId instanceId, int level, string blueprintId, int rightWeaponInstanceId, int leftWeaponInstanceId, int[] accessoryInstanceIds)
        {
            return new Player()
            {
                instanceId = instanceId.Issue,
                PlayerName = "",
                Level = level,
                BlueprintId = blueprintId,
                rightHand = new Hand(rightWeaponInstanceId),
                leftHand = new Hand(leftWeaponInstanceId),
                accessoryInstanceIds = accessoryInstanceIds
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
                leftHand = this.leftHand,
                accessoryInstanceIds = this.accessoryInstanceIds
            };
        }

        public bool CanEquipmentType(Constants.EquipmentType type)
        {
            return ((int)this.CharacterRecord.Job.Equipable & (int)type) != 0;
        }

        /// <summary>
        /// <paramref name="instanceId"/>を装備しているか返す
        /// </summary>
        public bool IsEquiped(int instanceId)
        {
            return this.RightHand.EquipmentInstanceId == instanceId ||
            this.LeftHand.EquipmentInstanceId == instanceId ||
            this.AccessoryInstanceIds.FindIndex(a => a == instanceId) >= 0;
        }

        /// <summary>
        /// <paramref name="equipmentId"/>と同じ武器を既に装備しているか返す
        /// </summary>
        public bool IsEquipedSameEquipment(string equipmentId) => (this.RightHand.IsPossession && this.RightHand.UserEquipment.Id == equipmentId) || (this.LeftHand.IsPossession && this.LeftHand.UserEquipment.Id == equipmentId);

        /// <summary>
        /// <paramref name="instanceId"/>のアクセサリーを装備しているか返す
        /// </summary>
        public bool IsEquipedAccessory(int instanceId) => this.AccessoryInstanceIds.FindIndex(a => a == instanceId) >= 0;

        /// <summary>
        /// 装備品を切り替える
        /// </summary>
        public void ChangeEquipment(Constants.HandType handType, int instanceId)
        {
            if(handType == Constants.HandType.Right)
            {
                this.RightHand.EquipmentInstanceId = instanceId;
            }
            else
            {
                this.LeftHand.EquipmentInstanceId = instanceId;
            }
        }

        /// <summary>
        /// アクセサリーを切り替える
        /// </summary>
        /// <param name="index"></param>
        /// <param name="instanceId"></param>
        public void ChangeAccessory(int index, int instanceId)
        {
            this.accessoryInstanceIds[index] = instanceId;
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

        public EquipmentRecord[] Accessories
        {
            get
            {
                var userData = UserData.Instance;
                return this.AccessoryInstanceIds.Select(instanceId =>
                {
                    if(instanceId == 0)
                    {
                        return null;
                    }
                    return MasterData.Equipment.GetById(userData.Equipments.GetByInstanceId(instanceId).Id);
                })
                .ToArray();
            }
        }

        /// <summary>
        /// バトルで利用するコマンドを返す
        /// </summary>
        public ConditionalCommandRecord[] UsingCommands => Calculator.GetCommandRecords(this.RightHand.EquipmentRecord, this.LeftHand.EquipmentRecord, this.Accessories);

        public ConditionalSkillElement[] ConditionalSkillElements => Calculator.GetSkillElements(this.RightHand.EquipmentRecord, this.LeftHand.EquipmentRecord, this.Accessories);
    }
}
