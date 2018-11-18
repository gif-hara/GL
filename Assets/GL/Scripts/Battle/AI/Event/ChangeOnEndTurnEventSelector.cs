using GL.Battle.CharacterControllers;
using UnityEngine;

namespace GL.Battle.AIControllers
{
    /// <summary>
    /// OnEndTurnイベントを切り替えるイベント
    /// </summary>
    [CreateAssetMenu(menuName = "GL/AI/Event/ChangeOnEndTurnEventSelector")]
    public sealed class ChangeOnEndTurnEventSelector : Event
    {
        [SerializeField]
        private int index;

        public override void Invoke(Character invoker)
        {
            invoker.AIController.ChangeOnEndTurnEventSelector(this.index);
        }
    }
}
