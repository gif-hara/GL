using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Command;
using HK.Framework.EventSystems;
using HK.GL.Battle;

namespace HK.GL.Events.Battle
{
    /// <summary>
    /// コマンドを実行する事を通知するイベント
    /// </summary>
    public sealed class InvokeCommand : Message<InvokeCommand, Character, ICommand>
    {
        /// <summary>
        /// 実行するヤーツ
        /// </summary>
        public Character Invoker{ get{ return this.param1; } }

        /// <summary>
        /// 実行するコマンド
        /// </summary>
        public ICommand Command{ get{ return this.param2; } }
    }
}
