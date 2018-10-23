using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Database
{
    /// <summary>
    /// マスターデータのレコードインターフェイス
    /// </summary>
    public interface IMasterDataRecord
    {
        string Id { get; }
    }
}
