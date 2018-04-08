using GL.Scripts.Battle.Systems;
using HK.Framework.EventSystems;

namespace HK.GL.Events.Battle
{
    /// <summary>
    /// ターン終了イベントが完了したことを通知する
    /// </summary>
    public sealed class CompleteEndTurnEvent : Message<CompleteEndTurnEvent>
    {
    }
}
