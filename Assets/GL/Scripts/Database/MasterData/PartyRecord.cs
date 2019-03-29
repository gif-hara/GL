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
        private PartyParameter[] parameters;

        [SerializeField]
        private Constants.CharacterType characterType;

        [SerializeField]
        private User.UnlockElements unlockElements;
        public User.UnlockElements UnlockElements => this.unlockElements;

        public string PartyName { get { return partyName.Get; } }

        public static PartyRecord CloneAsPlayerParty(User.Party party)
        {
            var clone = ScriptableObject.CreateInstance<PartyRecord>();
            clone.parameters = party.AsPlayers.Select(p => PartyParameter.Create(p)).ToArray();
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
                    p.CharacterRecord,
                    p.Commands,
                    p.SkillElements,
                    p.Level,
                    characterType
                );
                member.Add(character);
            }

            return new Battle.PartyControllers.Party(member, this);
        }

#if UNITY_EDITOR
        public void Set(StringAsset.Finder partyName, PartyParameter[] parameters, Constants.CharacterType characterType, UnlockElements unlockElements)
        {
            this.partyName = partyName;
            this.parameters = parameters;
            this.characterType = characterType;
            this.unlockElements = unlockElements;
        }
#endif
    }
}
