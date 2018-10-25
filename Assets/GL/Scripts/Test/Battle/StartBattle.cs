using UnityEngine;
using GL.Battle.CharacterControllers;
using GL.Battle.PartyControllers;
using GL.Battle;
using GL.Systems;
using GL.Database;

namespace GL.Test
{
    /// <summary>
    /// バトルを開始するテストクラス.
    /// </summary>
    public sealed class StartBattle : MonoBehaviour
    {
        [SerializeField]
        private PartyRecord playerParty;

        [SerializeField]
        private PartyRecord enemyParty;

        [SerializeField]
        private Character characterPrefab;

        [SerializeField]
        private Transform playerParent;

        [SerializeField]
        private Transform enemyParent;

        void Start()
        {
            var battlePlayerParty = SceneMediator.PlayerParty;
            var battleEnemyParty = SceneMediator.EnemyParty;

            battlePlayerParty = battlePlayerParty == null ? this.playerParty : battlePlayerParty;
            battleEnemyParty = battleEnemyParty == null ? this.enemyParty : battleEnemyParty;
            var parties = new Parties(
                battlePlayerParty.Create(this.characterPrefab, this.playerParent),
                battleEnemyParty.Create(this.characterPrefab, this.enemyParent)
            );

            BattleManager.Instance.Initialize(parties);
        }
    }
}
