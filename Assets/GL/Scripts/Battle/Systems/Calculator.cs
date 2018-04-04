using GL.Scripts.Battle.CharacterControllers;
using UnityEngine;

namespace GL.Scripts.Battle.Systems
{
    /// <summary>
    /// バトルで必要な計算を行う
    /// </summary>
    public static class Calculator
    {
        /// <summary>
        /// 通常攻撃でのダメージ計算を行う
        /// </summary>
        public static int GetBasicAttackDamage(CharacterStatusController invoker, CharacterStatusController target, float rate)
        {
            var baseStrength = Mathf.Pow(invoker.Status.Strength, 2) * rate;
            var baseDefense = Mathf.Pow(target.Status.Defense, 2);
            var result = Mathf.FloorToInt(baseStrength - baseDefense);
            return result < 1 ? 1 : result;
        }

        /// <summary>
        /// 防御力上昇系コマンドの上昇量を返す
        /// </summary>
        public static int GetAddDefenseValue(CharacterStatusController invoker)
        {
            return invoker.Status.Sympathy / 2;
        }
    }
}
