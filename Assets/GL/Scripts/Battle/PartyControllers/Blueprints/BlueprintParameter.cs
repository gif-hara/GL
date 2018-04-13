using System;
using GL.Scripts.Battle.Accessories;
using GL.Scripts.Battle.Weapons;
using GL.Scripts.User;
using UnityEngine;

namespace GL.Scripts.Battle.PartyControllers.Blueprints
{
    [Serializable]
    public class BlueprintParameter
    {
        [SerializeField][Range(1.0f, 100.0f)]
        public int Level;

        [SerializeField]
        public CharacterControllers.Blueprints.Blueprint Blueprint;

        [SerializeField]
        public Weapon Weapon;

        [SerializeField]
        public Accessory[] Accessories;

        public static BlueprintParameter Create(Player player)
        {
            return new BlueprintParameter()
            {
                Level = player.Level,
                Blueprint = player.Blueprint,
                Weapon = player.Weapon,
                Accessories = player.Accessories.ToArray()
            };
        }
    }
}
