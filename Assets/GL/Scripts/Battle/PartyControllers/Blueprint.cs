using System.Collections.Generic;
using System.Linq;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using HK.Framework.Text;
using UnityEngine;

namespace GL.Scripts.Battle.PartyControllers
{
    /// <summary>
    /// パーティの設計図
    /// </summary>
    [CreateAssetMenu(menuName = "GL/PartyControllers/Blueprint")]
    public class Blueprint : ScriptableObject
    {
        [SerializeField]
        private StringAsset.Finder partyName;

        [SerializeField]
        private Parameter[] parameters;

        [SerializeField]
        private Constants.CharacterType characterType;

        public string PartyName { get { return partyName.Get; } }

        public static Blueprint CloneAsPlayerParty(User.Party party)
        {
            var clone = ScriptableObject.CreateInstance<Blueprint>();
            clone.parameters = party.Players.Select(Parameter.Create).ToArray();
            clone.characterType = Constants.CharacterType.Player;

            return clone;
        }

        public Party Create(Character controllerPrefab, Transform parent, Vector3 interval, float scaleX)
        {
            var member = new List<Character>();
            for (int i = 0; i < this.parameters.Length; i++)
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

        protected Character CreateCharacter(
            int index,
            Character controllerPrefab,
            Transform parent,
            Vector3 position,
            float scaleX
        )
        {
            var parameter = this.parameters[index];
            var result = this.InternalCreateCharacter(controllerPrefab, parent, position, parameter.Blueprint.Model, scaleX);
            result.Initialize(
                parameter.Blueprint,
                parameter.Weapon.Commands.Select(c => c.Create()).ToArray(),
                parameter.Accessories,
                parameter.Level,
                characterType
                );

            return result;
        }

        protected Character InternalCreateCharacter(Character controllerPrefab, Transform parent, Vector3 position, GameObject modelPrefab, float scaleX)
        {
            var result = Instantiate(controllerPrefab, parent);
            result.transform.localPosition = position;
            var model = Instantiate(modelPrefab, result.transform);
            model.transform.localPosition = Vector3.zero;
            var scale = model.transform.localScale;
            scale.x = scaleX;
            model.transform.localScale = scale;

            return result;
        }
    }
}
