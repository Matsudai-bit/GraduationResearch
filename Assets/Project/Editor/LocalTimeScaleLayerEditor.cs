using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocalTimeScaleLayer))]
public class LocalTimeScaleLayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var scaleLayer = (LocalTimeScaleLayer)target;
        var defProp = serializedObject.FindProperty("m_definition");
        var maskProp = serializedObject.FindProperty("m_timeScaleLayerMask");

        EditorGUILayout.PropertyField(defProp);

        // Definition Ç™ê›íËÇ≥ÇÍÇƒÇ¢ÇΩÇÁ MaskField Çï\é¶
        if (scaleLayer.Definition != null)
        {
            var names = scaleLayer.Definition.TypeNames.ToArray();
            maskProp.intValue = EditorGUILayout.MaskField(
                "Speed Type", maskProp.intValue, names
            );
        }

        serializedObject.ApplyModifiedProperties();
    }
}