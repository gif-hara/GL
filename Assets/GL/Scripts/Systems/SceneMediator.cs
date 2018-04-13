using GL.Scripts.Battle.PartyControllers.Blueprints;

namespace GL.Scripts.Systems
{
    /// <summary>
    /// シーンを仲介するクラス
    /// </summary>
    public static class SceneMediator
    {
        /// <summary>
        /// バトルするプレイヤーパーティ
        /// </summary>
        public static Blueprint PlayerParty;
        
        /// <summary>
        /// バトルする敵パーティ
        /// </summary>
        public static Blueprint EnemyParty;
    }
}
