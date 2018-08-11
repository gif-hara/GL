﻿using System;
using GL.Battle.Accessories;
using GL.User;
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
        public Weapon Weapon;

        [SerializeField]
        public Accessory[] Accessories;

        public static Parameter Create(Player player)
        {
            return new Parameter()
            {
                Level = player.Level,
                Blueprint = player.Blueprint,
                Weapon = player.Weapon,
                Accessories = player.Accessories.ToArray()
            };
        }
    }
}