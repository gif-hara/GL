using UnityEngine;

namespace GL.Scripts.Battle.Commands.Element
{
    /// <summary>
    /// コマンド1個単位の挙動の設計図の抽象クラス
    /// </summary>
    public abstract class Blueprint : ScriptableObject
    {
        public abstract IImplement Create();
    }
}
