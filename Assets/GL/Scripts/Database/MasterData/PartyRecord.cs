using System.Collections.Generic;
using System.Linq;
using GL.Battle.CharacterControllers;
using GL.Battle;
using HK.Framework.Text;
using UnityEngine;
using GL.User;
using GL.Database;

namespace GL.Database
{
    /// <summary>
    /// パーティレコード
    /// </summary>
    [CreateAssetMenu(menuName = "GL/MasterData/Record/Party")]
    public class PartyRecord : ScriptableObject, IMasterDataRecord
    {
        public string Id => this.name;
        
        [SerializeField]
        private StringAsset.Finder partyName;

        [SerializeField]
        private Battle.PartyControllers.Parameter[] parameters;

        [SerializeField]
        private Constants.CharacterType characterType;

        [SerializeField]
        private User.UnlockElements unlockElements;
        public User.UnlockElements UnlockElements => this.unlockElements;

        public string PartyName { get { return partyName.Get; } }

        public static PartyRecord CloneAsPlayerParty(User.Party party)
        {
            var userData = UserData.Instance;
            var clone = ScriptableObject.CreateInstance<PartyRecord>();
            clone.parameters = party.AsPlayers.Select(p => Battle.PartyControllers.Parameter.Create(userData, p)).ToArray();
            clone.characterType = Constants.CharacterType.Player;

            return clone;
        }

        public Battle.PartyControllers.Party Create(Character controllerPrefab, Transform parent, Vector3 interval, float scaleX)
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

            return new Battle.PartyControllers.Party(member, this);
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
                parameter.Commands,
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
