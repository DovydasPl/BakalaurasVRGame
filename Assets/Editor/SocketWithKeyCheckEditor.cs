using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.XR.Interaction.Toolkit;

[CustomEditor(typeof(SocketWithCardCheck))]
public class SocketWithKeyCheckEditor : XRSocketInteractorEditor
{
    private SerializedProperty targetKey = null;

    protected override void OnEnable() {
        base.OnEnable();
        targetKey = serializedObject.FindProperty("targetKey");
    }

    protected override void DrawProperties() {
        EditorGUILayout.PropertyField(targetKey);
        base.DrawProperties();
    }

}
