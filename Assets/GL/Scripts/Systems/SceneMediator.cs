using GL.Database;

namespace GL.Systems
{
    /// <summary>
    /// シーンを仲介するクラス
    /// </summary>
    public static class SceneMediator
    {
        /// <summary>
        /// バトルするプレイヤーパーティ
        /// </summary>
        public static PartyRecord PlayerParty;
        
        /// <summary>
        /// バトルする敵パーティ
        /// </summary>
        public static PartyRecord EnemyParty;
    }
}
