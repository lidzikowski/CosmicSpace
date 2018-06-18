using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MapBackground))]
public class MapBackgroundDrawer : Editor
{
    SerializedProperty backgroundTypeProperty;
    SerializedProperty positionProperty;

    private void OnEnable()
    {
        backgroundTypeProperty = serializedObject.FindProperty("BackgroundType");
        positionProperty = serializedObject.FindProperty("Position");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(backgroundTypeProperty, new GUIContent("Background types"));
        EditorGUILayout.PropertyField(positionProperty, new GUIContent("Background types"));

        serializedObject.ApplyModifiedProperties();
    }
}