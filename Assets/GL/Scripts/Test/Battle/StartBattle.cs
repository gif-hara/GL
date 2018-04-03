using UnityEngine;
using System.Collections.Generic;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

namespace HK.GL.Test.Battle
{
    /// <summary>
    /// バトルを開始するテストクラス.
    /// </summary>
    public sealed class StartBattle : MonoBehaviour
    {
        [SerializeField]
        private Character controller;

        [SerializeField]
        private Transform playerParent;

        [SerializeField]
        private Transform enemyParent;

        [SerializeField]
        private Vector3 playerInterval;

        [SerializeField]
        private Vector3 enemyInterval;

        [SerializeField]
        private List<CharacterStatusSettings> players;

        [SerializeField]
        private List<CharacterStatusSettings> enemies;

        void Start()
        {
            var parties = new Parties(
                this.CreateParty(this.players, Constants.CharacterType.Player, this.playerParent, this.playerInterval, -1.0f),
                this.CreateParty(this.enemies, Constants.CharacterType.Enemy, this.enemyParent, this.enemyInterval, 1.0f)
            );

            BattleManager.Instance.Initialize(parties);
        }

        private Character CreateCharacter(
            CharacterStatusSettings settings,
            Constants.CharacterType characterType,
            Transform parent,
            Vector3 position,
            float scaleX
            )
        {
            var result = Instantiate(this.controller, parent);
            result.transform.localPosition = position;
            var model = Instantiate(settings.Model, result.transform);
            model.transform.localPosition = Vector3.zero;
            var scale = model.transform.localScale;
            scale.x = scaleX;
            model.transform.localScale = scale;
            result.Initialize(settings,characterType);

            return result;
        }

        private Party CreateParty(
            List<CharacterStatusSettings> settings,
            Constants.CharacterType characterType,
            Transform parent,
            Vector3 interval,
            float scaleX
            )
        {
            var member = new List<Character>();
            for(int i=0; i<settings.Count; i++)
            {
                member.Add(this.CreateCharacter(settings[i], characterType, parent, interval * i, scaleX));
            }

            return new Party(member);
        }
    }
}
