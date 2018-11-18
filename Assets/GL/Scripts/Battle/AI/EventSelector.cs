using System;
using GL.Battle.CharacterControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.AIControllers
{
    /// <summary>
    /// イベントを選択するクラス
    /// </summary>
    /// <remarks>
    /// <see cref="CommandSelector"/>とは違って条件を満たしているイベントは全て実行されます
    /// </remarks>
    [CreateAssetMenu(menuName = "GL/AI/EventSelector")]
    public sealed class EventSelector : ScriptableObject
    {
        [SerializeField]
        private Condition[] conditions;
        public Condition[] Conditions => this.conditions;

        [SerializeField]
        private Event[] events;
        public Event[] Events => this.events;

        public bool Suitable(Character invoker)
        {
            foreach (var c in this.conditions)
            {
                if (c.Suitable(invoker))
                {
                    return true;
                }
            }

            return false;
        }

#if UNITY_EDITOR
        public EventSelector Set(Condition[] conditions, Event[] events)
        {
            this.conditions = conditions;
            this.events = events;

            return this;
        }
#endif
    }
}
