using System.Collections.Generic;
using System.Linq;
using GL.MasterData;
using GL.User;
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

        void OnEnable()
        {
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode == false)
            {
                return;
            }
#endif

            // FPS設定
            {
                Application.targetFrameRate = this.targetFrameRate;
            }
            
            // ユーザーデータを読み込む
            {
                var userData = UserData.Load();
                if (userData.IsEmpty)
                {
                    userData.Initialize(this.userData);
                    userData.Save();
                }
            }

            // データベース登録
            {
                this.database.Setup();
            }
            
            // DontDestroyなゲームオブジェクトを生成する
            {
                Observable.NextFrame()
                    .Subscribe(_ =>
                    {
                        foreach (var prefab in this.dontDestroyPrefabs)
                        {
                            DontDestroyOnLoad(Instantiate(prefab));
                        }
                    });
            }
        }
    }
}
