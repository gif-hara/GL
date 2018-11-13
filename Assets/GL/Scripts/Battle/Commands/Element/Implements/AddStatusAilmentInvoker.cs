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
    public sealed class AddStatusAilmentInvoker : Implement<AddStatusAilmentInvoker.Parameter>
    {
        public AddStatusAilmentInvoker(Parameter parameter)
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
            var result = invoker.AilmentController.Add(this.parameter.RemainingTurn, this.parameter.StatusAilmentType, this.parameter.Rate);
            if (result && bundle.CanRecord)
            {
                BattleManager.Instance.InvokedCommandResult.AddAilments.Add(new InvokedCommandResult.AddAilment(invoker, this.parameter.StatusAilmentType));
            }
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
