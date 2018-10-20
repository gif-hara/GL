using GL.Battle.CharacterControllers;
using GL.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle
{
    /// <summary>
    /// バトルで必要な計算を行う
    /// </summary>
    public static class Calculator
    {
        /// <summary>
        /// 通常攻撃でのダメージ計算を行う
        /// </summary>
        public static int GetBasicAttackDamage(Character invoker, Character target, float rate)
        {
            // TODO: 実装
            var baseStrength = Mathf.Pow(invoker.StatusController.GetTotalParameter(Constants.StatusParameterType.Strength), 2) * rate;
            var baseDefense = Mathf.Pow(target.StatusController.GetTotalParameter(Constants.StatusParameterType.Defense), 2);
            var result = baseStrength - baseDefense;

            // クリティカルが発生したら1.5倍
            if (LotteryCritical(invoker, target))
            {
                result *= 1.5f;
            }
            
            // 攻撃者が鬼神化の場合は1.5倍
            if (invoker.AilmentController.Find(Constants.StatusAilmentType.Demonization))
            {
                result *= 1.5f;
            }
            
            // 対象が鬼神化の場合は1.5倍
            if (target.AilmentController.Find(Constants.StatusAilmentType.Demonization))
            {
                result *= 1.5f;
            }
            
            // 対象が睡眠の場合は1.5倍
            if (target.AilmentController.Find(Constants.StatusAilmentType.Sleep))
            {
                result *= 1.5f;
            }

            // 対象が硬質化の場合は半分になる
            if (target.AilmentController.Find(Constants.StatusAilmentType.Hardening))
            {
                result *= 0.5f;
            }
            
            return result < 1 ? 1 : Mathf.FloorToInt(result);
        }

        /// <summary>
        /// クリティカルが発生したか返す
        /// </summary>
        public static bool LotteryCritical(Character invoker, Character target)
        {
            // 対象が急所持ちなら必ずクリティカルが発生する
            if (target.AilmentController.Find(Constants.StatusAilmentType.Vitals))
            {
                return true;
            }
            
            return Random.value <= 0.5f;
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
                return Mathf.FloorToInt(rate);
            }
            else
            {
                return Mathf.FloorToInt(rate);
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
                return Mathf.FloorToInt(rate);
            }
            else
            {
                return Mathf.FloorToInt(rate);
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
                return Mathf.FloorToInt(rate);
            }
            else
            {
                return Mathf.FloorToInt(rate);
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

        /// <summary>
        /// 状態異常の毒によるダメージ量を返す
        /// </summary>
        public static int GetPoisonDamage(CharacterStatusController statusController)
        {
            return statusController.HitPointMax / 10;
        }

        /// <summary>
        /// 状態異常の再生による回復量を返す
        /// </summary>
        public static int GetRegenerationAmount(CharacterStatusController statusController)
        {
            return statusController.HitPointMax / 10;
        }

        /// <summary>
        /// 状態異常の憤怒による攻撃力上昇値を返す
        /// </summary>
        public static int GetRageAmount(CharacterStatusController statusController)
        {
            var result = statusController.Base.Parameter.Strength / 10;
            return result < 1 ? 1 : result;
        }

        /// <summary>
        /// 回復コマンドの回復量を返す
        /// </summary>
        public static int GetRecoveryAmount(Character invoker, float rate)
        {
            return Mathf.FloorToInt(rate);
        }
    }
}
