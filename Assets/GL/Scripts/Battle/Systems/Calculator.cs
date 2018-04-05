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
            var baseStrength = Mathf.Pow(invoker.TotalStrength, 2) * rate;
            var baseDefense = Mathf.Pow(target.TotalDefense, 2);
            var result = Mathf.FloorToInt(baseStrength - baseDefense);
            return result < 1 ? 1 : result;
        }

        /// <summary>
        /// 攻撃力上昇系コマンドの上昇量を返す
        /// </summary>
        public static int GetAddStrengthValue(CharacterStatusController invoker)
        {
            return invoker.TotalSympathy / 2;
        }

        /// <summary>
        /// 防御力上昇系コマンドの上昇量を返す
        /// </summary>
        public static int GetAddDefenseValue(CharacterStatusController invoker)
        {
            return invoker.TotalSympathy / 2;
        }

        /// <summary>
        /// 思いやり力上昇系コマンドの上昇量を返す
        /// </summary>
        public static int GetAddSympathyValue(CharacterStatusController invoker)
        {
            return invoker.TotalSympathy / 2;
        }
        
        /// <summary>
        /// ネガティブ力上昇系コマンドの上昇量を返す
        /// </summary>
        public static int GetAddNegaValue(CharacterStatusController invoker)
        {
            return invoker.TotalSympathy / 2;
        }
        
        /// <summary>
        /// 素早さ上昇系コマンドの上昇量を返す
        /// </summary>
        public static int GetAddSpeedValue(CharacterStatusController invoker)
        {
            return invoker.TotalSympathy / 2;
        }
    }
}
