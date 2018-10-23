using System.Collections.Generic;
using System.Linq;
using GL.DeveloperTools;
using GL.Database;
using GL.User;
using HK.Framework.Systems;
using UniRx;
using UnityEngine;

namespace GL.Systems
{
    /// <summary>
    /// アプリケーション起動時に実行されるクラス
    /// </summary>
    [CreateAssetMenu(menuName = "GL/Systems/BootController")]
    public sealed class BootController : ScriptableObject
    {
        [SerializeField]
        private int targetFrameRate;

        [SerializeField]
        private UserData userData;

        [SerializeField]
        private MasterData masterData;

        [SerializeField]
        private GameObject[] dontDestroyPrefabs;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var instance = Resources.Load<BootController>("BootController");

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
                instance.masterData.Setup();
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

            // SRDebuggerのSystemにGLの情報を登録する
            {
                var systemInformationService = new SystemInformationService();
                systemInformationService.Setup();
            }
        }
    }
}
