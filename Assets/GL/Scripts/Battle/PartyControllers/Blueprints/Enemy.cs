using System;
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
        private Parameter[] parameters;

        protected override Constants.CharacterType CharacterType { get { return Constants.CharacterType.Enemy; } }

        protected override BlueprintParameter[] Parameters { get { return this.parameters; } }

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
