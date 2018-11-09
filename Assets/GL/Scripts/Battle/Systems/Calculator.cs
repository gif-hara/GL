using System.Collections.Generic;
using GL.Battle.CharacterControllers;
using GL.Database;
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
        public static int GetBasicAttackDamage(Character invoker, Character target, float rate, Constants.AttributeType attributeType)
        {
            var baseStrength = Mathf.Pow(invoker.StatusController.GetTotalParameter(Constants.StatusParameterType.Strength), 1.48f) * rate;
            var baseDefense = Mathf.Pow(target.StatusController.GetTotalParameter(Constants.StatusParameterType.Defense), 1.39f);
            var result = baseStrength - baseDefense;
            var random = result / (10 + (invoker.StatusController.Level * 2));
            result += Random.Range(-random, random);

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

            // 属性の倍率を適用する
            var attributeRate = target.StatusController.GetTotalAttribute(attributeType);
            result *= attributeRate;

            // 属性値がマイナスの場合はダメージを吸収する
            if(attributeRate < 0.0f)
            {
                return Mathf.FloorToInt(result);
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

            var threshold = invoker.StatusController.GetTotalParameter(Constants.StatusParameterType.Critical).ToPercentage();
            return Random.value <= threshold;
        }

        /// <summary>
        /// 攻撃が当たるか返す
        /// </summary>
        public static bool IsHit(Character invoker, Character target)
        {
            var threshold = Mathf.Min(target.StatusController.GetTotalParameter(Constants.StatusParameterType.Avoidance).ToPercentage(), Constants.AvoidanceMax);
            return Random.value >= threshold;
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
                case Constants.StatusParameterType.StrengthMagic:
                    return GetAddStrengthMagicValue(invoker, rate);
                case Constants.StatusParameterType.Defense:
                    return GetAddDefenseValue(invoker, rate);
                case Constants.StatusParameterType.DefenseMagic:
                    return GetAddDefenseMagicValue(invoker, rate);
                case Constants.StatusParameterType.Speed:
                    return GetAddSpeedValue(invoker, rate);
                case Constants.StatusParameterType.Critical:
                    return GetAddCriticalValue(invoker, rate);
                case Constants.StatusParameterType.Avoidance:
                    return GetAddAvoidanceValue(invoker, rate);
                default:
                    Assert.IsTrue(false, $"{type}は未対応の値です");
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
        /// 魔法攻撃力上昇系コマンドの上昇量を返す
        /// </summary>
        private static int GetAddStrengthMagicValue(CharacterStatusController invoker, float rate)
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
        /// 魔法防御力上昇系コマンドの上昇量を返す
        /// </summary>
        private static int GetAddDefenseMagicValue(CharacterStatusController invoker, float rate)
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
        /// クリティカル率上昇系コマンドの上昇量を返す
        /// </summary>
        private static int GetAddCriticalValue(CharacterStatusController invoker, float rate)
        {
            return rate.ToPercentage();
        }

        /// <summary>
        /// 回避率上昇系コマンドの上昇量を返す
        /// </summary>
        private static int GetAddAvoidanceValue(CharacterStatusController invoker, float rate)
        {
            return rate.ToPercentage();
        }

        /// <summary>
        /// 状態異常を付与できるか抽選する
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

        /// <summary>
        /// 装備品から利用可能なコマンドを返す
        /// </summary>
        public static ConditionalCommandRecord[] GetCommandRecords(EquipmentRecord rightWeapon, EquipmentRecord leftWeapon, EquipmentRecord[] accessories)
        {
            var constantCommand = MasterData.ConstantCommand;
            var result = new List<ConditionalCommandRecord>();

            // 何も装備していないときは素手コマンドを追加
            if (rightWeapon == null && leftWeapon == null)
            {
                result.Add(constantCommand.Unequipment);
            }

            AddCommandRecords(result, rightWeapon, rightWeapon, leftWeapon, accessories);
            AddCommandRecords(result, leftWeapon, rightWeapon, leftWeapon, accessories);
            foreach (var a in accessories)
            {
                AddCommandRecords(result, a, rightWeapon, leftWeapon, accessories);
            }

            return result.ToArray();
        }

        private static void AddCommandRecords(
            List<ConditionalCommandRecord> result,
            EquipmentRecord target,
            EquipmentRecord rightWeapon,
            EquipmentRecord leftWeapon,
            EquipmentRecord[] accessories
            )
        {
            if(target == null)
            {
                return;
            }

            foreach (var c in target.Commands)
            {
                if (!result.Contains(c) && c.Condition.Suitable(rightWeapon, leftWeapon, accessories))
                {
                    result.Add(c);
                }
            }
        }
    }
}
