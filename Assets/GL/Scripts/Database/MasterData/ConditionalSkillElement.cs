using System;
using GL.Battle.Commands;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Database
{
    /// <summary>
    /// 条件付きの<see cref="Database.SkillElement"/>
    /// </summary>
    [Serializable]
    public sealed class ConditionalSkillElement
    {
        [SerializeField]
        private SkillElement skillElement;
        public SkillElement SkillElement => this.skillElement;

        [SerializeField]
        private EquipmentElementCondition condition;
        public EquipmentElementCondition Condition => this.condition;

#if UNITY_EDITOR
        public ConditionalSkillElement(SkillElement skillElement, EquipmentElementCondition condition)
        {
            this.skillElement = skillElement;
            this.condition = condition;
        }
#endif
    }
}
