using UnityEngine;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.PartyControllers;
using GL.Scripts.Battle.Systems;
using Blueprint = GL.Scripts.Battle.PartyControllers.Blueprint;

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
        private Blueprint playerParty;

        [SerializeField]
        private Blueprint enemyParty;

        void Start()
        {
            var parties = new Parties(
                this.playerParty.Create(this.controller, this.playerParent, this.playerInterval, -1.0f),
                this.enemyParty.Create(this.controller, this.enemyParent, this.enemyInterval, 1.0f)
            );

            BattleManager.Instance.Initialize(parties);
        }
    }
}
