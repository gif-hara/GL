using System.Collections.Generic;
using System.Linq;
using GL.Scripts.Battle.CharacterControllers;
using HK.Framework.EventSystems;
using HK.GL.Events.Battle;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Scripts.Battle.Systems
{
    /// <summary>
    /// バトルの行動順を制御するヤーツ.
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
            var elapseTurn = GetElapseTurn(allMember, waits, waitMax, speedMax);
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
            this.currentCharacter.StatusController.Wait -= waitMax;
        }

        public List<Character> Simulation(Parties paties, int length)
        {
            var result = new List<Character>();
            var allMember = paties.AllMember;
            var waits = new List<float>();
            var waitMax = allMember.Count;
            var speedMax = allMember.Max(c => c.StatusController.GetTotalParameter(Constants.StatusParameterType.Speed));

            for(int i=0; i<allMember.Count; i++)
            {
                waits.Add(allMember[i].StatusController.Wait);
            }

            while(result.Count <= length)
            {
                var elapseTurn = GetElapseTurn(allMember, waits, waitMax, speedMax);
                result.Add(allMember[elapseTurn.BehaviourCharacterIndex]);
                for(int i=0; i<allMember.Count; i++)
                {
                    waits[i] += ((float)allMember[i].StatusController.GetTotalParameter(Constants.StatusParameterType.Speed) / speedMax) * elapseTurn.TurnNumber;
                    if(elapseTurn.BehaviourCharacterIndex == i)
                    {
                        waits[i] -= waitMax;
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

        private static ElapseTurn GetElapseTurn(List<Character> allMember, List<float> waits, float waitMax, float speedMax)
        {
            var elapseTurn = new ElapseTurn();

            // すでに待機時間が満たされているキャラクターがいるか確認する
            elapseTurn.BehaviourCharacterIndex = FindAlreadyWaitMaxIndex(allMember, waits, waitMax);
            elapseTurn.TurnNumber = 0;

            // 待機時間が満たされていない場合は必要ターン数を計算する
            if(elapseTurn.BehaviourCharacterIndex < 0)
            {
                elapseTurn.BehaviourCharacterIndex = -1;
                elapseTurn.TurnNumber = int.MaxValue;
                for(int i=0; i<allMember.Count; i++)
                {
                    if(allMember[i].StatusController.IsDead)
                    {
                        continue;
                    }

                    var needTurn = GetNeedTurn(waitMax, waits[i], speedMax, allMember[i].StatusController.GetTotalParameter(Constants.StatusParameterType.Speed));
                    if(elapseTurn.TurnNumber > needTurn)
                    {
                        elapseTurn.TurnNumber = needTurn;
                        elapseTurn.BehaviourCharacterIndex = i;
                    }
                }
            }

            Assert.AreNotEqual(elapseTurn.BehaviourCharacterIndex, -1);

            return elapseTurn;
        }

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
