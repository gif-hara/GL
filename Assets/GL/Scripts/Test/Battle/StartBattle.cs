using UnityEngine;
using GL.Battle.CharacterControllers;
using GL.Battle.PartyControllers;
using GL.Battle.Systems;
using GL.Systems;

namespace GL.Test
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
        private GL.Battle.PartyControllers.Blueprint playerParty;

        [SerializeField]
        private GL.Battle.PartyControllers.Blueprint enemyParty;

        void Start()
        {
            var battlePlayerParty = SceneMediator.PlayerParty;
            var battleEnemyParty = SceneMediator.EnemyParty;

            battlePlayerParty = battlePlayerParty == null ? this.playerParty : battlePlayerParty;
            battleEnemyParty = battleEnemyParty == null ? this.enemyParty : battleEnemyParty;
            var parties = new Parties(
                battlePlayerParty.Create(this.controller, this.playerParent, this.playerInterval, -1.0f),
                battleEnemyParty.Create(this.controller, this.enemyParent, this.enemyInterval, 1.0f)
            );

            BattleManager.Instance.Initialize(parties);
        }
    }
}
