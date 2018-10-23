using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Database
{
    /// <summary>
    /// リスト形式で保持するデータベースのインターフェイス
    /// </summary>
    public interface IDataBaseList<T> where T : ScriptableObject
    {
        T[] List { get; }
    }
}
