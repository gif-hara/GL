using System;
using GL.Battle;
using GL.MasterData;
using GL.User;
using HK.GL.Extensions;
using UnityEngine;

namespace GL.Battle.PartyControllers
{
    /// <summary>
    /// パーティに必要なパラメータ
    /// </summary>
    [Serializable]
    public class Parameter
    {
        [SerializeField][Range(1.0f, 100.0f)]
        public int Level;

        [SerializeField]
        public CharacterControllers.Blueprint Blueprint;

        [SerializeField]
        public Battle.Weapon Weapon;

        [SerializeField]
        public Battle.Accessory[] Accessories;

        public static Parameter Create(UserData userData, Player player)
        {
            return new Parameter()
            {
                Level = player.Level,
                Blueprint = player.Blueprint,
                Weapon = Database.Weapon.List.Find(w => w.Id == userData.Weapons[player.WeaponId].Id),
                Accessories = player.Accessories.ToArray()
            };
        }
    }
}
