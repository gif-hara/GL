using GL.Scripts.Battle.Commands.Implements;
using UnityEngine;

namespace GL.Scripts.Battle.Commands.Blueprints
{
    /// <summary>
    /// コマンドを構成する設計図の抽象クラス
    /// </summary>
    public abstract class Blueprint : ScriptableObject
    {
        public abstract IImplement Create();
    }
}
