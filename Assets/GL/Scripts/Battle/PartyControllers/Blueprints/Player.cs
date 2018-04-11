using System;
using System.Collections.Generic;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using UnityEngine;

namespace GL.Scripts.Battle.PartyControllers.Blueprints
{
    /// <summary>
    /// プレイヤーパーティの設計図
    /// </summary>
    [CreateAssetMenu(menuName = "GL/PartyControllers/Blueprints/Player")]
    public sealed class Player : Blueprint
    {
        [SerializeField]
        private List<Parameter> parameters = new List<Parameter>();
        public List<Parameter> Parameters { get { return parameters; } }

        protected override Constants.CharacterType CharacterType { get { return Constants.CharacterType.Player; } }

        public override Party Create(Character controllerPrefab, Transform parent, Vector3 interval, float scaleX)
        {
            var member = new List<Character>();
            for (int i = 0; i < this.parameters.Count; i++)
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
            private CharacterControllers.Blueprints.Player player;

            public override CharacterControllers.Blueprints.Blueprint Character { get { return this.player; } }
        }
    }
}
