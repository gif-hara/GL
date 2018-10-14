using UnityEngine;
using UnityEngine.Assertions;

namespace GL.User
{
    /// <summary>
    /// インスタンスIDを持つインターフェイス
    /// </summary>
    public interface IInstanceId
    {
        int InstanceId { get; }
    }
}
