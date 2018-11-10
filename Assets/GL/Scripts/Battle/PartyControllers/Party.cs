using System;
using System.Collections.Generic;
using System.Linq;
using GL.Battle.CharacterControllers;
using GL.Battle;
using HK.GL.Extensions;
using UnityEngine.Assertions;
using GL.Database;

namespace GL.Battle.PartyControllers
{
    /// <summary>
    /// パーティを管理するクラス
    /// </summary>
    public sealed class Party
    {
        public List<Character> Members { private set; get; }

        public PartyRecord PartyRecord { get; private set; }

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

        public Party(List<Character> members, PartyRecord blueprint)
        {
            this.Members = members;
            this.PartyRecord = blueprint;
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
        /// <remarks>
        /// <see cref="Constants.TargetType.Select"/>の場合も選択可能なターゲット全てを返します
        /// その中から任意のターゲットを選択する処理を別個で実装する必要があります
        /// </remarks>
        /// <param name="invoker">ターゲットを選択するキャラクター</param>
        /// <param name="type">ターゲットしたいタイプ</param>
        public Character[] GetTargets(Character invoker, Constants.TargetType type)
        {
            var targets = this.SurvivalMembers;
            
            // 鎌鼬化の場合は全体化する
            if (invoker.AilmentController.Find(Constants.StatusAilmentType.Sickle))
            {
                return targets.ToArray();
            }
            
            var result = new List<Character>();
            switch(type)
            {
                case Constants.TargetType.Select: // Selectの場合も全てのリストを返す
                case Constants.TargetType.SelectRange:
                case Constants.TargetType.All:
                case Constants.TargetType.Random: // Randomもコマンド実行時にターゲットを選択する
                    result.AddRange(targets);
                    break;
                case Constants.TargetType.Myself:
                    result.Add(invoker);
                    break;
                case Constants.TargetType.OnChaseTakeDamages:
                    result.AddRange(BattleManager.Instance.InvokedCommandResult.TakeDamages.Select(x => x.Target));
                    break;
                case Constants.TargetType.OnCounterTakeDamages:
                    result.AddRange(BattleManager.Instance.InvokedCommandResult.TakeDamages.Select(x => x.Invoker));
                    break;
                default:
                    Assert.IsTrue(false, string.Format("未対応の値です {0}", type));
                    break;
            }

            return result.ToArray();
        }
    }
}
