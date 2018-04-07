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
            // TODO: 実装
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
            // TODO: 実装
            return invoker.TotalSympathy / 2;
        }

        /// <summary>
        /// 防御力上昇系コマンドの上昇量を返す
        /// </summary>
        public static int GetAddDefenseValue(CharacterStatusController invoker)
        {
            // TODO: 実装
            return invoker.TotalSympathy / 2;
        }

        /// <summary>
        /// 思いやり力上昇系コマンドの上昇量を返す
        /// </summary>
        public static int GetAddSympathyValue(CharacterStatusController invoker)
        {
            // TODO: 実装
            return invoker.TotalSympathy / 2;
        }
        
        /// <summary>
        /// ネガティブ力上昇系コマンドの上昇量を返す
        /// </summary>
        public static int GetAddNegaValue(CharacterStatusController invoker)
        {
            // TODO: 実装
            return invoker.TotalSympathy / 2;
        }
        
        /// <summary>
        /// 素早さ上昇系コマンドの上昇量を返す
        /// </summary>
        public static int GetAddSpeedValue(CharacterStatusController invoker)
        {
            // TODO: 実装
            return invoker.TotalSympathy / 2;
        }

        /// <summary>
        /// 状態異常をかけられるか抽選する
        /// </summary>
        public static bool LotteryStatusAilment(float rate)
        {
            // TODO: 抽選処理
            return true;
        }
    }
}
