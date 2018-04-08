using HK.Framework.EventSystems;

namespace GL.Scripts.Events.Battle
{
    /// <summary>
    /// ターン終了イベントが完了したことを通知する
    /// </summary>
    public sealed class CompleteEndTurnEvent : Message<CompleteEndTurnEvent>
    {
    }
}
