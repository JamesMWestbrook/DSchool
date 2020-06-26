using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(ScheduledAI))]
public class ScheduledAIEditor : Editor
{
    public bool ShowNormal = true;

    public bool WeekOne = true;
    public bool WeekTwo = true;

    public override void OnInspectorGUI()
    {
        SerializedProperty daysList = serializedObject.FindProperty("Days");
        ShowNormal = EditorGUILayout.Toggle("ShowNormal", ShowNormal);
        //    if(daysList.objectReferenceValue != null)
        if (ShowNormal)
        {
            Debug.Log("Shownormal");
            WeekOne = EditorGUILayout.Foldout(WeekOne, "Week 1");
            if (WeekOne)
            {
                ShowArrayProperty(daysList, 0);
            }
            WeekTwo = EditorGUILayout.Foldout(WeekTwo, "Week 2");
            if (WeekTwo)
            {
                ShowArrayProperty(daysList, 8);
            }
            serializedObject.ApplyModifiedProperties();
        }
        else
        {
            base.OnInspectorGUI();
            return;
        }
    }

    public void ShowArrayProperty(SerializedProperty list, int startValue)
    {//Thanks Neogen13 on unity forums, this saved my life

        EditorGUI.indentLevel++;
        for (int i = startValue; i < startValue + 7; i++)
        {
            Debug.Log(i);
            EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent("Day " + (i + 1)));
        }
        EditorGUI.indentLevel--;
    }

}


namespace DecayingDev
{
    using UnityEditor;

    public static class Editor
    {
        public static void ShowArrayProperty(SerializedProperty list)
        {//Thanks Neogen13 on unity forums, this saved my life
            EditorGUILayout.PropertyField(list, false);
            EditorGUI.indentLevel++;
            for (int i = 0; i < list.arraySize; i++)
            {
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), new GUIContent("Day " + (i + 1)));
            }
            EditorGUI.indentLevel--;
        }
    }

}