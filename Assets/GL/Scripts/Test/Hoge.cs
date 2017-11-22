using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.GL.Events;
using UniRx;

namespace HK.GL.Test
{
	/// <summary>
	/// .
	/// </summary>
	public class Hoge : MonoBehaviour
	{
		void Awake()
		{
            GLEvent.GlobalBroker.Receive<GLTestEvent>()
                .Subscribe(g => Debug.Log("Hoge = " + g.Value));
        }
	}
}
