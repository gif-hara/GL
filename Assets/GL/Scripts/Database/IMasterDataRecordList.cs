using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Database
{
    /// <summary>
    /// レコードを保持するインターフェイス
    /// </summary>
    public interface IMasterDataRecordList<T> where T : ScriptableObject, IMasterDataRecord
    {
        T[] List { get; }

        T GetById(string id);
    }
}
