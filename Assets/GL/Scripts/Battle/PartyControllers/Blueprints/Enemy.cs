using System;
using System.Collections.Generic;
using GL.Scripts.Battle.Accessories;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using GL.Scripts.Battle.Weapons;
using UnityEngine;

namespace GL.Scripts.Battle.PartyControllers.Blueprints
{
    /// <summary>
    /// 敵パーティの設計図
    /// </summary>
    [CreateAssetMenu(menuName = "GL/PartyControllers/Blueprints/Enemy")]
    public sealed class Enemy : Blueprint
    {
        [SerializeField]
        private List<Parameter> parameters = new List<Parameter>();
        public List<Parameter> Parameters { get { return parameters; } }

        protected override Constants.CharacterType CharacterType { get { return Constants.CharacterType.Enemy; } }

        public override Party Create(Character controllerPrefab, Transform parent, Vector3 interval, float scaleX)
        {
            var member = new List<Character>();
            for (int i = 0; i < this.Parameters.Count; i++)
            {
                var character = this.CreateCharacter(
                    i,
                    controllerPrefab,
                    parent,
                    interval * i,
                    scaleX
                );
                member.Add(character);
            }

            return new Party(member);
        }

        protected override Character CreateCharacter(
            int index,
            Character controllerPrefab,
            Transform parent,
            Vector3 position,
            float scaleX
        )
        {
            var parameter = parameters[index];
            var result = this.InternalCreateCharacter(controllerPrefab, parent, position, parameter.Character.Model, scaleX);
            result.Initialize(parameter.Character, parameter.Level, CharacterType);

            return result;
        }

        [Serializable]
        public class Parameter : BlueprintParameter
        {
            [SerializeField]
            private CharacterControllers.Blueprints.Enemy enemy;

            [SerializeField]
            public Weapon Weapon;

            [SerializeField]
            private Accessory[] Accessories;

            public override CharacterControllers.Blueprints.Blueprint Character
            {
                get
                {
                    enemy.Accessories = this.Accessories;
                    enemy.Commands = this.Weapon.Commands;
                    return this.enemy;
                }
            }
        }
    }
}
