using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GL.User
{
    /// <summary>
    /// 実体を持つデータ
    /// </summary>
    [Serializable]
    public class InstanceData<T> where T : class, IInstanceId
    {
        [SerializeField]
        public List<T> List = new List<T>();

        [SerializeField][HideInInspector]
        public InstanceId InstanceId = new InstanceId();

        public T GetByInstanceId(int instanceId)
        {
            if(instanceId == 0)
            {
                return default(T);
            }
            
            var result = this.List.Find(t => t.InstanceId == instanceId);
            Assert.IsNotNull(result, $"{instanceId}に紐づくデータの取得に失敗しました");

            return result;
        }
    }

    /// <summary>
    /// インスペクターで編集出来るように定義されたインスタンスデータ
    /// </summary>
    public class InstanceData
    {
        [Serializable]
        public class Player : InstanceData<User.Player>{}

        [Serializable]
        public class Party : InstanceData<User.Party>{}

        [Serializable]
        public class Weapon : InstanceData<User.Equipment>{}

        [Serializable]
        public class Accessory : InstanceData<User.Accessory>{}
    }
}
