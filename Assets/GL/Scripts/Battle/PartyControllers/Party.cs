using System;
using System.Collections.Generic;
using System.Linq;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using HK.GL.Extensions;
using UnityEngine.Assertions;

namespace GL.Scripts.Battle.PartyControllers
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
        /// <remarks>
        /// <see cref="Constants.TargetType.Select"/>の場合も選択可能なターゲット全てを返します
        /// その中から任意のターゲットを選択する処理を別個で実装する必要があります
        /// </remarks>
        /// <param name="invoker">ターゲットを選択するキャラクター</param>
        /// <param name="type">ターゲットしたいタイプ</param>
        /// <param name="takeDamage">ダメージを伴う行動を行うか</param>
        public List<Character> GetTargets(Character invoker, Constants.TargetType type, bool takeDamage)
        {
            var targets = this.SurvivalMembers;
            var protectCharacters = targets.FindAll(c => c.AilmentController.Find(Constants.StatusAilmentType.Protect));
            var canProtect = takeDamage && protectCharacters.Count > 0;
            Character protectCharacter = null;
            if (canProtect)
            {
                protectCharacter = protectCharacters[UnityEngine.Random.Range(0, protectCharacters.Count)];
            }
            
            // 鎌鼬化の場合は全体化する
            if (invoker.AilmentController.Find(Constants.StatusAilmentType.Sickle))
            {
                return targets;
            }
            
            var result = new List<Character>();
            switch(type)
            {
                case Constants.TargetType.Select: // Selectの場合も全てのリストを返す
                case Constants.TargetType.All:
                    result.AddRange(targets);
                    break;
                case Constants.TargetType.Random:
                    var random = targets[UnityEngine.Random.Range(0, targets.Count)];
                    if (canProtect && random != protectCharacter)
                    {
                        result.Add(protectCharacter);
                        // TODO: 庇う発動したことを通知する
                    }
                    else
                    {
                        result.Add(random);
                    }
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
