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
        public static Battle.PartyControllers.Blueprint PlayerParty;
        
        /// <summary>
        /// バトルする敵パーティ
        /// </summary>
        public static Battle.PartyControllers.Blueprint EnemyParty;
    }
}
