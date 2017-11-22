using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.GL.Extensions;
using System;
using System.Linq;

namespace HK.GL.Battle
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
                return this.Members.Where(m => !m.Status.IsDead).ToList();
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
        /// <param name="type">ターゲットしたいタイプ</param>
        /// <param name="characters">ターゲットリスト</param>
        /// <param name="selector">ターゲットする際の抽選方法</param>
        public List<Character> GetTargets(Constants.TargetType type, Func<Character, int> selector)
        {
            var result = new List<Character>();
            var targets = this.SurvivalMembers;
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
                default:
                    Assert.IsTrue(false, "未対応のTargetTypeです");
                    break;
            }

            return result;
        }
    }
}
