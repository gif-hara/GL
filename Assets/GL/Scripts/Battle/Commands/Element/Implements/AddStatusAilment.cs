using System;
using System.Linq;
using GL.Battle.CharacterControllers;
using GL.Battle;
using GL.Extensions;
using HK.GL.Extensions;
using Random = UnityEngine.Random;

namespace GL.Battle.Commands.Element.Implements
{
    /// <summary>
    /// 状態異常を追加するコマンド.
    /// </summary>
    public sealed class AddStatusAilment : Implement<AddStatusAilment.Parameter>
    {
        public AddStatusAilment(Parameter parameter)
            : base(parameter)
        {
        }

        public override bool TakeDamage
        {
            get
            {
                return false;
            }
        }

        public override void Invoke(Character invoker, Commands.Bundle.Implement bundle, Character[] targets)
        {
            targets.ForEach(t =>
            {
                var result = t.AilmentController.Add(this.parameter.RemainingTurn, this.parameter.StatusAilmentType, this.parameter.Rate);
                if(result && bundle.CanRecord)
                {
                    BattleManager.Instance.InvokedCommandResult.AddAilments.Add(new InvokedCommandResult.AddAilment(t, this.parameter.StatusAilmentType));
                }
            });
        }

        [Serializable]
        public class Parameter : IParameter
        {
            /// <summary>
            /// 付与する状態異常タイプ
            /// </summary>
            public Constants.StatusAilmentType StatusAilmentType;

            /// <summary>
            /// 状態異常を付与する確率
            /// </summary>
            public float Rate;
        
            public int RemainingTurn;
        }
    }
}
