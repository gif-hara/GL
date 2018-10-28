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
            clone.parameters = party.AsPlayers.Select(p => Battle.PartyControllers.Parameter.Create(p)).ToArray();
            clone.characterType = Constants.CharacterType.Player;

            return clone;
        }

        public Battle.PartyControllers.Party Create(Character prefab, Transform parent)
        {
            var member = new List<Character>();
            foreach(var p in this.parameters)
            {
                var character = Instantiate(prefab, parent);
                character.Initialize(
                    p.Blueprint,
                    p.Commands,
                    p.Accessories,
                    p.Level,
                    characterType
                );
                member.Add(character);
            }

            return new Battle.PartyControllers.Party(member, this);
        }
    }
}
