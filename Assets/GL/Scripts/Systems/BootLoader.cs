﻿using System.Collections.Generic;
using System.Linq;
using GL.MasterData;
using GL.User;
using HK.Framework.Systems;
using UniRx;
using UnityEngine;

namespace GL.Systems
{
    /// <summary>
    /// 一番最初に処理するクラス
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Systems/BootLoader")]
    public sealed class BootLoader : ScriptableObject
    {
        [SerializeField]
        private int targetFrameRate;

        [SerializeField]
        private UserData userData;

        [SerializeField]
        private Database database;

        [SerializeField]
        private GameObject[] dontDestroyPrefabs;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var instance = Resources.Load<BootLoader>("BootLoader");

            // FPS設定
            {
                Application.targetFrameRate = instance.targetFrameRate;
            }
            
            // ユーザーデータを読み込む
            {
                var userData = UserData.Instance;
                if (userData.IsEmpty)
                {
                    userData.Initialize(instance.userData);
                    userData.Save();
                }
            }

            // データベース登録
            {
                instance.database.Setup();
            }
            
            // DontDestroyなゲームオブジェクトを生成する
            {
                Observable.NextFrame()
#if UNITY_EDITOR
                    .Where(_ => Application.isPlaying)
#endif
                    .Subscribe(_ =>
                    {
                        foreach (var prefab in instance.dontDestroyPrefabs)
                        {
                            DontDestroyOnLoad(Instantiate(prefab));
                        }
                    });
            }
        }
    }
}
