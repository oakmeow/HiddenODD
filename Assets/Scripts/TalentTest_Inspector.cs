using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(TalentTest))]
public class TalentTest_Inspector : Editor
{
    SerializedProperty once;
    SerializedProperty skip1;
    SerializedProperty accelator;

    SerializedProperty prefixStateName;
    SerializedProperty colliderAsStateName;

    SerializedProperty order;

    private void OnEnable()
    {
        once = serializedObject.FindProperty("once");
        skip1 = serializedObject.FindProperty("skip1");
        accelator = serializedObject.FindProperty("accelator");

        prefixStateName = serializedObject.FindProperty("prefixStateName");
        colliderAsStateName = serializedObject.FindProperty("colliderAsStateName");

        order = serializedObject.FindProperty("order");

        //intA = serializedObject.FindProperty("a");
        //intB = serializedObject.FindProperty("b");
    }


    public override void OnInspectorGUI()
    {
        //serializedObject.Update();

        //EditorGUILayout.PropertyField(intB);


        //DrawDefaultInspector();

        //TalentTest myScript = (TalentTest)target;

        //GUILayout.Space();
        //EditorGUILayout.PropertyField(once);
        //EditorGUILayout.PropertyField(skip1);
        //EditorGUILayout.PropertyField(accelator);
        //EditorGUILayout.PropertyField(prefixStateName);
        //EditorGUILayout.PropertyField(colliderAsStateName);

        //[Tooltip("Skip frame 1 after round 2")]
        if (GUILayout.Button("Test"))
        {
            Debug.Log("Test จ้า");
        }

        //EditorGUILayout.PropertyField(order);
        DrawDefaultInspector();

        //EditorGUILayout.PropertyField(intA);
    }


    /*public override VisualElement CreateInspectorGUI()
    {
        VisualElement myInspector = new VisualElement();

        myInspector.Add(new Button());

        return myInspector;
    }*/
}
