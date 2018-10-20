using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class UndopectorWindow : EditorWindow
{
    private static UndopectorWindow instance;

    [SerializeField]
    private Texture undoImage;

    [SerializeField]
    private Texture redoImage;

    private List<int> instanceIds = new List<int>();

    private int currentIndex = 0;

    private bool registerSelection = true;

    [MenuItem("Window/Undopector")]
    private static void Open()
    {
        if(instance == null)
        {
            instance = CreateInstance<UndopectorWindow>();
        }

        instance.titleContent.text = "Undopector";
        instance.minSize = new Vector2(80, 24);
        instance.ShowUtility();
    }

    void OnGUI()
    {
        using(new GUILayout.HorizontalScope())
        {
            var buttonGUILayoutOption = new GUILayoutOption[]
            {
                GUILayout.Width(24),
                GUILayout.Height(18),
            };

            EditorGUI.BeginDisabledGroup(this.currentIndex <= 0);
            if (GUILayout.Button(this.undoImage, buttonGUILayoutOption))
            {
                this.registerSelection = false;
                this.currentIndex--;
                Selection.activeInstanceID = this.instanceIds[this.currentIndex];
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(this.currentIndex >= this.instanceIds.Count - 1);
            if (GUILayout.Button(this.redoImage, buttonGUILayoutOption))
            {
                this.registerSelection = false;
                this.currentIndex++;
                Selection.activeInstanceID = this.instanceIds[this.currentIndex];
            }
            EditorGUI.EndDisabledGroup();
        }
    }

    void OnSelectionChange()
    {
        var instanceId = Selection.activeInstanceID;
        if(this.registerSelection && instanceId != 0)
        {
            if (this.currentIndex < this.instanceIds.Count - 1)
            {
                this.instanceIds = new List<int>(this.instanceIds.GetRange(0, this.currentIndex + 1));
            }

            if(this.instanceIds.Count <= 0 || this.instanceIds[this.instanceIds.Count - 1] != instanceId)
            {
                this.instanceIds.Add(instanceId);
                this.currentIndex = this.instanceIds.Count - 1;
            }
        }

        this.registerSelection = true;
        this.Repaint();
    }
}
