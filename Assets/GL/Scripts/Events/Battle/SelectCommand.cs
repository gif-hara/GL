using GL.Scripts.Battle.CharacterControllers;
using HK.Framework.EventSystems;

namespace GL.Scripts.Events.Battle
{
    /// <summary>
    /// コマンドを選択するのを通知するイベント
    /// </summary>
    public sealed class SelectCommand : Message<SelectCommand, Character>
    {
        /// <summary>
        /// 次に行動するキャラクターを返す
        /// </summary>
        public Character Character { get { return this.param1; } }
    }
}
