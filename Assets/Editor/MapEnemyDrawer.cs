using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MapEnemy))]
public class MapEnemyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float width = position.width / 3;
        var enemyShipRect = new Rect(position.x, position.y, 2 * width, position.height);
        var countRect = new Rect(position.x + (2 * width), position.y, width, position.height);

        EditorGUI.PropertyField(enemyShipRect, property.FindPropertyRelative("EnemyType"), GUIContent.none);
        EditorGUI.PropertyField(countRect, property.FindPropertyRelative("Count"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}