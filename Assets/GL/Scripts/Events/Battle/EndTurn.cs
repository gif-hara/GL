using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.GL.Battle;

namespace HK.GL.Events.Battle
{
    /// <summary>
    /// ターンが終了した際に通知するイベント
    /// </summary>
    public sealed class EndTurn : GLEvent<EndTurn>
    {
    }
}
