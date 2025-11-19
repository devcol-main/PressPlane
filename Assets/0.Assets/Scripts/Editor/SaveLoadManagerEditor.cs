using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(SaveLoadManager)),CanEditMultipleObjects]
public class SaveLoadManagerEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        SaveLoadManager saveLoadManager = (SaveLoadManager) target;
        
        DrawDefaultInspector();

        if(GUILayout.Button("Save Game"))
        {
            saveLoadManager.Save();
        }

        if(GUILayout.Button("Load Game"))
        {
            saveLoadManager.Load();
        }

        if(GUILayout.Button("Delete Game"))
        {
            saveLoadManager.Delete();
        }
    }
}
