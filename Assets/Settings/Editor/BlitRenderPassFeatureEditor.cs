using System;
using UnityEngine;
using UnityEditor;

public class BlitRenderPassFeatureEditor : EditorWindow
{
    public bool toggle;

    [MenuItem("Window/Blit RenderPass Settings")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        BlitRenderPassFeatureEditor window = (BlitRenderPassFeatureEditor)EditorWindow.GetWindow(typeof(BlitRenderPassFeatureEditor));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Blit RenderPass Settings", EditorStyles.boldLabel);
        GUILayout.Space(20);
        toggle = EditorGUILayout.Toggle("Effect On Editor Camera", toggle);
        BlitRenderPassFeature.onEditorCameraRenderFeature = toggle;
    }

    private void OnValidate()
    {
    }
}
