using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Commands.Implements;
using HK.Framework.EventSystems;

namespace HK.GL.Events.Battle
{
    /// <summary>
    /// コマンドを実行する事を通知するイベント
    /// </summary>
    public sealed class InvokeCommand : Message<InvokeCommand, Character, IImplement>
    {
        /// <summary>
        /// 実行するヤーツ
        /// </summary>
        public Character Invoker{ get{ return this.param1; } }

        /// <summary>
        /// 実行するコマンド
        /// </summary>
        public IImplement Command{ get{ return this.param2; } }
    }
}
