using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.GL.Events;
using HK.GL.Events.Battle;
using UniRx;
using UnityEngine.UI;
using HK.GL.Battle;
using System.Text;

namespace HK.GL.Test
{
    /// <summary>
    /// 行動順を表示するデバッグ
    /// </summary>
    public sealed class DrawBehavioralOrder : MonoBehaviour
    {
        private Text text;

        void Awake()
        {
            this.text = this.GetComponent<Text>();
            Assert.IsNotNull(this.text);

            GLEvent.GlobalBroker.Receive<BehavioralOrderSimulationed>()
                .Select(b => b.Order)
                .Subscribe(this.Draw)
                .AddTo(this);
        }

        private void Draw(List<Character> order)
        {
            var stringBuilder = new StringBuilder();
            order.ForEach(o => stringBuilder.AppendLine(o.Status.Name));

            this.text.text = stringBuilder.ToString();
        }
    }
}
