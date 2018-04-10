using System;
using System.Collections.Generic;
using System.Linq;
using GL.Scripts.Battle.Systems;
using HK.GL.Extensions;
using UnityEngine.Assertions;

namespace GL.Scripts.Battle.CharacterControllers
{
    /// <summary>
    /// パーティを管理するヤーツ.
    /// </summary>
    public sealed class Party
    {
        public List<Character> Members { private set; get; }

        /// <summary>
        /// 生き残っているメンバーを返す
        /// </summary>
        public List<Character> SurvivalMembers
        {
            get
            {
                return this.Members.Where(m => !m.StatusController.IsDead).ToList();
            }
        }

        public Party(List<Character> members)
        {
            this.Members = members;
        }

        /// <summary>
        /// メンバーを追加する
        /// </summary>
        public void Add(Character character)
        {
            this.Members.Add(character);
        }

        /// <summary>
        /// ターゲットタイプからターゲットを返す
        /// </summary>
        /// <param name="invoker">ターゲットを選択するキャラクター</param>
        /// <param name="type">ターゲットしたいタイプ</param>
        /// <param name="selector">ターゲットする際の抽選方法</param>
        public List<Character> GetTargets(Character invoker, Constants.TargetType type, Func<Character, int> selector)
        {
            var targets = this.SurvivalMembers;
            
            // 鎌鼬化の場合は全体化する
            if (invoker.AilmentController.Find(Constants.StatusAilmentType.Sickle))
            {
                return targets;
            }
            
            var result = new List<Character>();
            switch(type)
            {
                case Constants.TargetType.Strong:
                    result.Add(targets.FindMax(selector));
                    break;
                case Constants.TargetType.Weak:
                    result.Add(targets.FindMin(selector));
                    break;
                case Constants.TargetType.All:
                    result.AddRange(targets);
                    break;
                case Constants.TargetType.Random:
                    result.Add(targets[UnityEngine.Random.Range(0, targets.Count)]);
                    break;
                case Constants.TargetType.Myself:
                    result.Add(invoker);
                    break;
                case Constants.TargetType.OnChaseTakeDamages:
                    result.AddRange(BattleManager.Instance.InvokedCommandResult.TakeDamages.Select(x => x.Target));
                    break;
                default:
                    Assert.IsTrue(false, string.Format("未対応の値です {0}", type));
                    break;
            }

            return result;
        }
    }
}
