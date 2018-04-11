using System.Collections.Generic;
using GL.Scripts.User;
using UnityEngine;

namespace GL.Scripts.Systems
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
        private Party initialParty;
                
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
                    userData.Initialize(this.initialParty);
                }
            }
        }
    }
}
