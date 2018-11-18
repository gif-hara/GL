using GL.Battle.CharacterControllers;
using UnityEngine;

namespace GL.Battle.AIControllers
{
    /// <summary>
    /// コマンド選択リストを切り替えるイベント
    /// </summary>
    [CreateAssetMenu(menuName = "GL/AI/Event/ChangeCommandSelector")]
    public sealed class ChangeCommandSelector : Event
    {
        [SerializeField]
        private int index;

        public override void Invoke(Character invoker)
        {
            invoker.AIController.ChangeCommandSelector(this.index);
        }
    }
}
