using System.Collections.Generic;
using System.Linq;
using GL.Battle.CharacterControllers;
using GL.Battle.PartyControllers;
using GL.Events.Battle;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Battle.Systems
{
    /// <summary>
    /// バトルの行動順を制御するクラス.
    /// </summary>
    public sealed class BehavioralOrderController : MonoBehaviour
    {
        [SerializeField]
        private int simulationLength;

        private Character currentCharacter;

        void Awake()
        {
            Broker.Global.Receive<NextTurn>()
                .SubscribeWithState(this, (n, _this) => _this.currentCharacter = n.NextCharacter)
                .AddTo(this);
        }

        /// <summary>
        /// ターン経過処理を行う
        /// </summary>
        /// <remarks>
        /// 今回行動したキャラクターのwaitをリセットする
        /// </remarks>
        public void Elapse(Parties parties)
        {
            var allMember = parties.AllMember;
            var waits = allMember.Select(m => m.StatusController.Wait).ToList();
            var waitMax = allMember.Count;
            var speedMax = allMember.Max(c => c.StatusController.GetTotalParameter(Constants.StatusParameterType.Speed));
            var usedPreEmpts = allMember.Select(m => 0).ToList();
            var elapseTurn = GetElapseTurn(allMember, waits, waitMax, speedMax, usedPreEmpts);
            for(int i=0; i<allMember.Count; i++)
            {
                allMember[i].StatusController.Wait += ((float)allMember[i].StatusController.GetTotalParameter(Constants.StatusParameterType.Speed) / speedMax) * elapseTurn.TurnNumber;
            }
        }

        /// <summary>
        /// ターン終了処理を行う
        /// </summary>
        public void EndTurn(Parties parties)
        {
            var waitMax = parties.AllMember.Count;
            var wait = this.currentCharacter.StatusController.Wait;
            this.currentCharacter.StatusController.Wait = Mathf.Max(wait - waitMax, 0.0f);
        }

        public List<Character> Simulation(Parties paties, int length)
        {
            var result = new List<Character>();
            var allMember = paties.AllMember;
            var waits = new List<float>();
            var waitMax = allMember.Count;
            var speedMax = allMember.Max(c => c.StatusController.GetTotalParameter(Constants.StatusParameterType.Speed));
            var usedPreEmpts = new List<int>();

            for(int i=0; i<allMember.Count; i++)
            {
                waits.Add(allMember[i].StatusController.Wait);
                usedPreEmpts.Add(0);
            }

            while(result.Count <= length)
            {
                var elapseTurn = GetElapseTurn(allMember, waits, waitMax, speedMax, usedPreEmpts);
                result.Add(allMember[elapseTurn.BehaviourCharacterIndex]);
                for(int i=0; i<allMember.Count; i++)
                {
                    waits[i] += ((float)allMember[i].StatusController.GetTotalParameter(Constants.StatusParameterType.Speed) / speedMax) * elapseTurn.TurnNumber;
                    if(elapseTurn.BehaviourCharacterIndex == i)
                    {
                        waits[i] = Mathf.Max(waits[i] - waitMax, 0.0f);
                    }
                }
            }

            Broker.Global.Publish(BehavioralOrderSimulationed.Get(result));
            return result;
        }

        private static int GetNeedTurn(float waitMax, float wait, float speedMax, float speed)
        {
            return Mathf.CeilToInt(Mathf.Max((waitMax - wait) / (speed / speedMax), 0.0f));
        }

        private static ElapseTurn GetElapseTurn(List<Character> allMember, List<float> waits, float waitMax, float speedMax, List<int> usedPreEmpts)
        {
            var elapseTurn = new ElapseTurn();
            
            // 先制を持っているキャラクターがいる場合は優先して行動できる
            elapseTurn.BehaviourCharacterIndex = FindPreEmptIndex(allMember, usedPreEmpts);
            if (elapseTurn.BehaviourCharacterIndex != -1)
            {
                return elapseTurn;
            }

            // すでに待機時間が満たされているキャラクターがいるか確認する
            elapseTurn.BehaviourCharacterIndex = FindAlreadyWaitMaxIndex(allMember, waits, waitMax);
            if (elapseTurn.BehaviourCharacterIndex != -1)
            {
                return elapseTurn;
            }

            // 待機時間が満たされていない場合は必要ターン数を計算する
            elapseTurn.BehaviourCharacterIndex = -1;
            elapseTurn.TurnNumber = int.MaxValue;
            for (int i = 0; i < allMember.Count; i++)
            {
                if (allMember[i].StatusController.IsDead)
                {
                    continue;
                }

                var needTurn = GetNeedTurn(waitMax, waits[i], speedMax, allMember[i].StatusController.GetTotalParameter(Constants.StatusParameterType.Speed));
                if (elapseTurn.TurnNumber > needTurn)
                {
                    elapseTurn.TurnNumber = needTurn;
                    elapseTurn.BehaviourCharacterIndex = i;
                }
            }

            Assert.AreNotEqual(elapseTurn.BehaviourCharacterIndex, -1);

            return elapseTurn;
        }

        /// <summary>
        /// 先制持ちのキャラクターのインデックスを返す
        /// </summary>
        /// <param name="allMember">バトルに参加している全てのメンバー</param>
        /// <param name="usedPreEmpts">すでに先制を利用したか</param>
        private static int FindPreEmptIndex(List<Character> allMember, List<int> usedPreEmpts)
        {
            const Constants.StatusAilmentType preEmpt = Constants.StatusAilmentType.PreEmpt;
            for (var i = 0; i < allMember.Count; i++)
            {
                var member = allMember[i];
                if (!member.AilmentController.Find(preEmpt))
                {
                    continue;
                }
                if(member.AilmentController.Get(preEmpt).RemainingTurn > usedPreEmpts[i])
                {
                    usedPreEmpts[i]++;
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// すでにWaitが最大値まで溜まっているキャラクターのインデックスを返す
        /// </summary>
        /// <param name="allMember">バトルに参加している全てのメンバー</param>
        /// <param name="waits">メンバーのWait</param>
        /// <param name="waitMax">Waitの最大値</param>
        private static int FindAlreadyWaitMaxIndex(List<Character> allMember, List<float> waits, float waitMax)
        {
            Assert.AreEqual(allMember.Count, waits.Count);
            for(int i=0; i<allMember.Count; i++)
            {
                if(waits[i] >= waitMax && !allMember[i].StatusController.IsDead)
                {
                    return i;
                }
            }

            return -1;
        }

        private class ElapseTurn
        {
            public int BehaviourCharacterIndex { set; get; }

            public int TurnNumber{ set; get; }

            public ElapseTurn()
            {
                this.Reset();
            }

            public void Reset()
            {
                this.BehaviourCharacterIndex = 0;
                this.TurnNumber = 0;
            }
        }
    }
}
