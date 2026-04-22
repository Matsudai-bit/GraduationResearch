using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpeedTag))]
public class SpeedTagEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var tag = (SpeedTag)target;
        var defProp = serializedObject.FindProperty("m_definition");
        var maskProp = serializedObject.FindProperty("m_speedTypeMask");

        EditorGUILayout.PropertyField(defProp);

        // Definition Ç™ê›íËÇ≥ÇÍÇƒÇ¢ÇΩÇÁ MaskField Çï\é¶
        if (tag.Definition != null)
        {
            var names = tag.Definition.TypeNames.ToArray();
            maskProp.intValue = EditorGUILayout.MaskField(
                "Speed Type", maskProp.intValue, names
            );
        }

        serializedObject.ApplyModifiedProperties();
    }
}