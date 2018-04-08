using System;
using System.Collections.Generic;
using System.Linq;
using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;
using GL.Scripts.Extensions;
using HK.Framework.EventSystems;
using HK.GL.Events.Battle;
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
        private readonly CompositeDisposable disposable = new CompositeDisposable();

        private readonly GUIStyle textStyle = new GUIStyle();

        private List<string> behavioralNames = new List<string>();

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
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                var window = CreateInstance<CharacterStatusWindow>();
                window.ShowUtility();
                Broker.Global.Receive<BehavioralOrderSimulationed>()
                    .SubscribeWithState(window, (x, w) =>
                    {
                        w.behavioralNames = x.Order.Select(c => c.StatusController.Name).ToList();
                    })
                    .AddTo(window.disposable);
            }
            
            else if (state == PlayModeStateChange.EnteredEditMode)
            {
                var window = GetWindow<CharacterStatusWindow>();
                window.Close();
                window.disposable.Clear();
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
            
            this.DrawCharacterStatus(battleManager.Parties.AllMember);
            this.DrawBehavioralNames();
        }

        private void DrawCharacterStatus(IEnumerable<Character> characters)
        {
            var statusControllers = characters.Select(c => c.StatusController);
            var ailmentControllers = characters.Select(c => c.AilmentController);
            GUILayout.Label("Parameter");
            using (new GUILayout.HorizontalScope())
            {
                this.DrawStatus(statusControllers, "Name", s => s.Name);
                this.DrawStatus(statusControllers, "HP", s => string.Format("{0}/{1}", s.HitPoint, s.HitPointMax));
                this.DrawStatus(statusControllers, "STR", s => s.GetTotalParameter(Constants.StatusParameterType.Strength).ToString());
                this.DrawStatus(statusControllers, "DEF", s => s.GetTotalParameter(Constants.StatusParameterType.Defense).ToString());
                this.DrawStatus(statusControllers, "SYM", s => s.GetTotalParameter(Constants.StatusParameterType.Sympathy).ToString());
                this.DrawStatus(statusControllers, "NEG", s => s.GetTotalParameter(Constants.StatusParameterType.Nega).ToString());
                this.DrawStatus(statusControllers, "SPD", s => s.GetTotalParameter(Constants.StatusParameterType.Speed).ToString());
                this.DrawStatus(statusControllers, "WAT", s => s.Wait.ToString());
                this.DrawStatusAilment(ailmentControllers, "状態異常");
            }
            GUILayout.Label("Resistance");
            using (new GUILayout.HorizontalScope())
            {
                this.DrawStatus(statusControllers, "Name", s => s.Name);
                this.DrawStatus(statusControllers, "Poison", s => s.GetTotalResistance(Constants.StatusAilmentType.Poison).ToString("0.00"));
                this.DrawStatus(statusControllers, "Paralysis", s => s.GetTotalResistance(Constants.StatusAilmentType.Paralysis).ToString("0.00"));
                this.DrawStatus(statusControllers, "Sleep", s => s.GetTotalResistance(Constants.StatusAilmentType.Sleep).ToString("0.00"));
                this.DrawStatus(statusControllers, "Confuse", s => s.GetTotalResistance(Constants.StatusAilmentType.Confuse).ToString("0.00"));
                this.DrawStatus(statusControllers, "Berserk", s => s.GetTotalResistance(Constants.StatusAilmentType.Berserk).ToString("0.00"));
            }
        }

        private void DrawBehavioralNames()
        {
            var tempColor = this.textStyle.normal.textColor;
            this.textStyle.alignment = TextAnchor.MiddleLeft;
            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                for (var i = 0; i < this.behavioralNames.Count; i++)
                {
                    var behavioralName = this.behavioralNames[i];
                    this.textStyle.normal.textColor = i == 0 ? Color.magenta : Color.black;
                    GUILayout.Label(behavioralName, this.textStyle);
                }
            }
            this.textStyle.normal.textColor = tempColor;
        }

        private void DrawStatus(IEnumerable<CharacterStatusController> statusControllers, string header, Func<CharacterStatusController, string> labelFunc)
        {
            var tempColor = this.textStyle.normal.textColor;
            this.textStyle.alignment = TextAnchor.MiddleRight;
            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                GUILayout.Label(header);
                foreach (var statusController in statusControllers)
                {
                    this.textStyle.normal.textColor = statusController.IsDead ? Color.red : Color.black;
                    
                    GUILayout.Label(labelFunc(statusController), this.textStyle);
                    this.textStyle.normal.textColor = tempColor;
                }
            }
        }

        private void DrawStatusAilment(
            IEnumerable<CharacterAilmentController> ailmentControllers,
            string header
            )
        {
            var tempColor = this.textStyle.normal.textColor;
            this.textStyle.alignment = TextAnchor.MiddleRight;
            using (new GUILayout.VerticalScope(GUI.skin.box))
            {
                GUILayout.Label(header);
                foreach (var ailmentController in ailmentControllers)
                {
                    using (new GUILayout.HorizontalScope())
                    {
                        if (ailmentController.Elements.Count <= 0)
                        {
                            this.textStyle.normal.textColor = tempColor;
                            GUILayout.Label("None", this.textStyle);
                        }
                        else
                        {
                            foreach (var element in ailmentController.Elements)
                            {
                                this.textStyle.normal.textColor = element.Type.IsPositive() ? Color.green : Color.red;
                    
                                GUILayout.Label(string.Format("{0}({1})", element.Type, element.RemainingTurn), this.textStyle);
                            }
                        }
                    }
                }
            }
            this.textStyle.normal.textColor = tempColor;
        }
    }
}
