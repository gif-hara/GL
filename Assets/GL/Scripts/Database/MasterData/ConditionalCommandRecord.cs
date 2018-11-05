using System;
using GL.Battle.Commands;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Database
{
    /// <summary>
    /// 条件付きの<see cref="CommandRecord"/>
    /// </summary>
    [Serializable]
    public sealed class ConditionalCommandRecord
    {
        [SerializeField]
        private CommandRecord commandRecord;
        public CommandRecord CommandRecord => this.commandRecord;

        [SerializeField]
        private EquipmentElementCondition condition;
        public EquipmentElementCondition Condition => this.condition;

#if UNITY_EDITOR
        public ConditionalCommandRecord(CommandRecord commandRecord, EquipmentElementCondition condition)
        {
            this.commandRecord = commandRecord;
            this.condition = condition;
        }
#endif
    }
}
