﻿using UnityEngine;
using UnityEngine.Assertions;

namespace GL.Database
{
    /// <summary>
    /// リスト形式で保持するマスターデータのインターフェイス
    /// </summary>
    public interface IMasterDataList<T> where T : ScriptableObject, IMasterDataRecord
    {
        T[] List { get; }

        T GetById(string id);
    }
}
