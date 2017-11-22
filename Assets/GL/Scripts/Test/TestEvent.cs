using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using UniRx;
using UnityEngine.EventSystems;
using System;
using HK.GL.Events;

namespace HK.GL.Test
{
	/// <summary>
	/// テストイベント.
	/// </summary>
	public class TestEvent : MonoBehaviour, IPointerClickHandler
	{
        public IMessageBroker Broker{ private set; get; }

		void Awake()
		{
            this.Broker = new MessageBroker();
        }

		void Start()
		{
            this.Broker.Receive<GLTestEvent>()
                .Subscribe(g => Debug.Log(g.Value))
				.AddTo(this);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            GLEvent.GlobalBroker.Publish<GLTestEvent>(GLTestEvent.Get(10));
        }
    }

	public class GLTestEvent : GLEvent<GLTestEvent, int>
	{
		public int Value{ get { return this.param1; } }
    }
}
