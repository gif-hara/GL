using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.GL.Battle;

namespace HK.GL.Events.Battle
{
    /// <summary>
    /// 次のターンに進んだことを通知するイベント
    /// </summary>
    public sealed class NextTurn : GLEvent<NextTurn, List<Character>>
    {
        /// <summary>
        /// 行動順
        /// </summary>
        public List<Character> Order { get{ return this.param1; } }

        /// <summary>
        /// 次に行動するキャラクターを返す
        /// </summary>
        public Character NextCharacter{ get{ return this.Order[0]; } }
    }
}
