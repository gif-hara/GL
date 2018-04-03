using GL.Scripts.Battle.CharacterControllers;

namespace HK.GL.Battle
{
    /// <summary>
    /// コマンド処理で使う便利機能
    /// </summary>
    public static class CommandUtility
    {
        /// <summary>
        /// 引数のキャラクターと敵対するパーティを返す
        /// </summary>
        public static Party GetOpponent(Character character, BattleParty battleParty)
        {
            return character.CharacterType == Constants.CharacterType.Player
                ? battleParty.Player : battleParty.Enemy;
        }
    }
}
