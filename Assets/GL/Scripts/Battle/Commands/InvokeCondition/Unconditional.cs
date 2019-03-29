using GL.Battle.CharacterControllers;
using GL.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle
{
    /// <summary>
    /// 無条件でコマンドを実行出来るクラス
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Commands/InvokeConditions/Unconditional")]
    public sealed class Unconditional : CommandInvokeCondition
    {
        public override Character[] Suitable(BattleManager battleManager, Character invoker, CommandRecord command)
        {
            return battleManager.Parties.GetFromTargetPartyType(invoker, command.Parameter.TargetPartyType).SurvivalMembers.ToArray();
        }
    }
}
