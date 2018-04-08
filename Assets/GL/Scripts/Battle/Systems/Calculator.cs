using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

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
        /// <paramref name="type"/>からパラメータ加算値を返す
        /// </summary>
        public static int GetAddStatusParameterValue(Constants.StatusParameterType type, CharacterStatusController invoker, float rate)
        {
            switch (type)
            {
                case Constants.StatusParameterType.Strength:
                    return GetAddStrengthValue(invoker, rate);
                case Constants.StatusParameterType.Defense:
                    return GetAddDefenseValue(invoker, rate);
                case Constants.StatusParameterType.Sympathy:
                    return GetAddSympathyValue(invoker, rate);
                case Constants.StatusParameterType.Nega:
                    return GetAddNegaValue(invoker, rate);
                case Constants.StatusParameterType.Speed:
                    return GetAddSpeedValue(invoker, rate);
                default:
                    Assert.IsTrue(false, string.Format("未対応の値です {0}", type));
                    return 0;
            }
        }

        /// <summary>
        /// 攻撃力上昇系コマンドの上昇量を返す
        /// </summary>
        private static int GetAddStrengthValue(CharacterStatusController invoker, float rate)
        {
            // TODO: 実装
            if (rate >= 0.0f)
            {
                return Mathf.FloorToInt(invoker.TotalSympathy / 2.0f * rate);
            }
            else
            {
                return Mathf.FloorToInt(invoker.TotalNega / 2.0f * rate);
            }
        }

        /// <summary>
        /// 防御力上昇系コマンドの上昇量を返す
        /// </summary>
        private static int GetAddDefenseValue(CharacterStatusController invoker, float rate)
        {
            // TODO: 実装
            if (rate >= 0.0f)
            {
                return Mathf.FloorToInt(invoker.TotalSympathy / 2.0f * rate);
            }
            else
            {
                return Mathf.FloorToInt(invoker.TotalNega / 2.0f * rate);
            }
        }

        /// <summary>
        /// 思いやり力上昇系コマンドの上昇量を返す
        /// </summary>
        private static int GetAddSympathyValue(CharacterStatusController invoker, float rate)
        {
            // TODO: 実装
            if (rate >= 0.0f)
            {
                return Mathf.FloorToInt(invoker.TotalSympathy / 2.0f * rate);
            }
            else
            {
                return Mathf.FloorToInt(invoker.TotalNega / 2.0f * rate);
            }
        }
        
        /// <summary>
        /// ネガキャン力上昇系コマンドの上昇量を返す
        /// </summary>
        private static int GetAddNegaValue(CharacterStatusController invoker, float rate)
        {
            // TODO: 実装
            if (rate >= 0.0f)
            {
                return Mathf.FloorToInt(invoker.TotalSympathy / 2.0f * rate);
            }
            else
            {
                return Mathf.FloorToInt(invoker.TotalNega / 2.0f * rate);
            }
        }
        
        /// <summary>
        /// 素早さ上昇系コマンドの上昇量を返す
        /// </summary>
        private static int GetAddSpeedValue(CharacterStatusController invoker, float rate)
        {
            // TODO: 実装
            if (rate >= 0.0f)
            {
                return Mathf.FloorToInt(invoker.TotalSympathy / 2.0f * rate);
            }
            else
            {
                return Mathf.FloorToInt(invoker.TotalNega / 2.0f * rate);
            }
        }

        /// <summary>
        /// 状態異常をかけられるか抽選する
        /// </summary>
        public static bool LotteryStatusAilment(CharacterStatusController target, Constants.StatusAilmentType statusAilmentType, float rate)
        {
            // 有利な状態異常は必ずかかる
            if (statusAilmentType.IsPositive())
            {
                return true;
            }
            
            return Random.value <= (rate - target.GetTotalResistance(statusAilmentType));
        }
    }
}
