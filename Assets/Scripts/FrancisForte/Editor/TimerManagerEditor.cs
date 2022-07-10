using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TimerManager))]
public class TimerManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TimerManager manager = (TimerManager)target;

        GUIStyle style = new GUIStyle(EditorStyles.miniButton);
        style.fixedHeight = 50;

        if (GUILayout.Button("Trigger Pause", style))
        {
            manager.TriggerPause();
        }

        if (GUILayout.Button("Trigger Restart", style))
        {
            manager.TriggerRestart();
        }

        if (GUILayout.Button("Trigger Time Up", style))
        {
            manager.TriggerTimeUp();
        }
    }
}
