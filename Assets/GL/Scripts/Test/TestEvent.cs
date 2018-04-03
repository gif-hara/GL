using UnityEngine;
using UniRx;
using UnityEngine.EventSystems;
using HK.Framework.EventSystems;

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
	        Framework.EventSystems.Broker.Global.Publish(GLTestEvent.Get(10));
        }
    }

	public class GLTestEvent : Message<GLTestEvent, int>
	{
		public int Value{ get { return this.param1; } }
    }
}
