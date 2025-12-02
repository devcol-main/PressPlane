using UnityEngine;
using UnityEditor;

// ReadOnlyDrawer.cs (must be in an Editor folder)

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false; // Disable GUI interaction

        // Draw the property field using the standard EditorGUI.
        // It will appear grayed out and uneditable.
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true; // Re-enable GUI interaction
    }

    // Override GetPropertyHeight if you need to adjust the height of the property.
    // In this case, the default height is usually sufficient.
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}
