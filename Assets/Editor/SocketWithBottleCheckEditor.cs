using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.XR.Interaction.Toolkit;

[CustomEditor(typeof(SocketWithBottleCheck))]
public class SocketWithBottleCheckEditor : XRSocketInteractorEditor
{
    private SerializedProperty targetGameObject = null;
    private SerializedProperty bottlePlaceSound = null;


    protected override void OnEnable() {
        base.OnEnable();
        targetGameObject = serializedObject.FindProperty("targetGameObject");
        bottlePlaceSound = serializedObject.FindProperty("bottlePlaceSound");

    }

    protected override void DrawProperties() {
        EditorGUILayout.PropertyField(targetGameObject);
        EditorGUILayout.PropertyField(bottlePlaceSound);

        base.DrawProperties();
    }

}
