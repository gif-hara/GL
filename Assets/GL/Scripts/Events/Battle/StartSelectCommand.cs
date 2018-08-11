using GL.Battle.CharacterControllers;
using HK.Framework.EventSystems;

namespace GL.Scripts.Events.Battle
{
    /// <summary>
    /// コマンドを選択開始を通知するイベント
    /// </summary>
    public sealed class StartSelectCommand : Message<StartSelectCommand, Character>
    {
        /// <summary>
        /// 次に行動するキャラクターを返す
        /// </summary>
        public Character Character { get { return this.param1; } }
    }
}
