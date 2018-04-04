using System;
using System.Collections.Generic;
using System.Linq;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace GL.DevelopTools.Scripts
{
    /// <summary>
    /// バトルに参加しているキャラクターのステータスを表示する<see cref="EditorWindow"/>
    /// </summary>
    public sealed class CharacterStatusWindow : EditorWindow
    {
        private CompositeDisposable disposable = new CompositeDisposable();

        [MenuItem("Window/GL/CharacterStatus")]
        private static void GetWindow()
        {
            GetWindow<CharacterStatusWindow>(true, "CharacterStatus", true);
        }

        [InitializeOnLoadMethod]
        private static void InitializeOnLoad()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                var window = CreateInstance<CharacterStatusWindow>();
                window.ShowUtility();
            }
            
            else if (state == PlayModeStateChange.ExitingPlayMode)
            {
                var window = GetWindow<CharacterStatusWindow>();
                window.Close();
            }
        }

        void Update()
        {
            this.Repaint();
        }

        void OnGUI()
        {
            if (!Application.isPlaying)
            {
                EditorGUILayout.LabelField("Application not Playing...");
                return;
            }

            var battleManager = BattleManager.Instance;
            if (battleManager == null)
            {
                return;
            }
            
            this.DrawCharacterStatus(battleManager.Parties.AllMember.Select(c => c.StatusController));
        }

        private void DrawCharacterStatus(IEnumerable<CharacterStatusController> statusControllers)
        {
            using (var rootHorizontal = new GUILayout.HorizontalScope())
            {
                this.DrawStatus(statusControllers, "Name", s => s.Name);
                this.DrawStatus(statusControllers, "HP", s => string.Format("{0}/{1}", s.HitPoint, s.HitPointMax));
                this.DrawStatus(statusControllers, "STR", s => s.TotalStrength.ToString());
                this.DrawStatus(statusControllers, "DEF", s => s.TotalDefense.ToString());
                this.DrawStatus(statusControllers, "SYM", s => s.TotalSympathy.ToString());
                this.DrawStatus(statusControllers, "NEG", s => s.TotalNega.ToString());
                this.DrawStatus(statusControllers, "SPD", s => s.TotalSpeed.ToString());
                this.DrawStatus(statusControllers, "WAT", s => s.Wait.ToString());
            }
//            using (var scope = new EditorGUILayout.HorizontalScope(GUI.skin.box, GUILayout.ExpandWidth(false)))
//            {
//                GUILayout.Label(string.Format("{0} HP:{1}/{2} STR:{3} DEF:{4} SYM:{5} NEG:{6} SPD:{7} WAT:{8}",
//                    statusController.BaseStatus.Name,
//                    statusController.HitPointMax,
//                    statusController.HitPoint,
//                    statusController.TotalStrength,
//                    statusController.TotalDefense,
//                    statusController.TotalSympathy,
//                    statusController.TotalNega,
//                    statusController.TotalSpeed,
//                    statusController.Wait),
//                    GUILayout.ExpandWidth(false));
//            }
        }

        private void DrawStatus(IEnumerable<CharacterStatusController> statusControllers, string header, Func<CharacterStatusController, string> labelFunc)
        {
            using (var scope = new GUILayout.VerticalScope(GUI.skin.box))
            {
                GUILayout.Label(header);
                foreach (var statusController in statusControllers)
                {
                    GUILayout.Label(labelFunc(statusController));
                }
            }
        }
    }
}
