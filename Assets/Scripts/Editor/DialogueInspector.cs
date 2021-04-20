using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor
{
    private SerializedProperty _type;
    private SerializedProperty _characterName;
    private SerializedProperty _sentence;

    private void OnEnable()
    {
        _type = serializedObject.FindProperty("type");
        _characterName = serializedObject.FindProperty("characterName");
        _sentence = serializedObject.FindProperty("sentence");
    }

    /*public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_type, new GUIContent("Dialogue Type"));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_characterName, new GUIContent("Character Name Displayed"));
        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(_sentence, new GUIContent("Sentence"), GUILayout.Height(90));
        EditorGUILayout.Space(10);
        Dialogue.TypeProp type = (Dialogue.TypeProp) _type.enumValueIndex;
        switch (type)
        {
            case Dialogue.TypeProp.Normal:

                SerializedProperty property = serializedObject.FindProperty("nextDialogue");
                EditorGUILayout.PropertyField(property, new GUIContent("Next Dialogue"), false);

                break;

            case Dialogue.TypeProp.Branch:

                SerializedProperty choices = serializedObject.FindProperty("choices");
                for (int i = 0; i < choices.arraySize; i++)
                {
                    SerializedProperty branchText =
                        choices.GetArrayElementAtIndex(i).FindPropertyRelative("choiceText");
                    SerializedProperty actionType = choices.GetArrayElementAtIndex(i).FindPropertyRelative("action");
                    SerializedProperty successNextDialogue =
                        choices.GetArrayElementAtIndex(i).FindPropertyRelative("successDialogue");
                    SerializedProperty failedNextDialogue =
                        choices.GetArrayElementAtIndex(i).FindPropertyRelative("failedDialogue");
                    SerializedProperty nextDialogue =
                        choices.GetArrayElementAtIndex(i).FindPropertyRelative("nextDialogue");

                    EditorGUILayout.BeginFoldoutHeaderGroup(true, "Branch " + i);
                    EditorGUILayout.PropertyField(branchText, new GUIContent("Option Text"));
                    EditorGUILayout.PropertyField(actionType, new GUIContent("Action Type"));

                    switch (actionType.enumValueIndex)
                    {
                        case 0:

                            EditorGUILayout.PropertyField(nextDialogue, new GUIContent("Next Dialogue"));
                            break;

                        case 1:

                            EditorGUILayout.PropertyField(successNextDialogue,
                                new GUIContent("Next Dialogue - Succeed"));
                            EditorGUILayout.PropertyField(failedNextDialogue, new GUIContent("Next Dialogue - Failed"));

                            break;

                        case 2:

                            EditorGUILayout.PropertyField(successNextDialogue,
                                new GUIContent("Next Dialogue - Succeed"));
                            EditorGUILayout.PropertyField(failedNextDialogue, new GUIContent("Next Dialogue - Failed"));

                            break;

                        case 3:

                            EditorGUILayout.PropertyField(successNextDialogue,
                                new GUIContent("Next Dialogue - Succeed"));
                            EditorGUILayout.PropertyField(failedNextDialogue, new GUIContent("Next Dialogue - Failed"));

                            break;

                        case 4:

                            EditorGUILayout.PropertyField(successNextDialogue,
                                new GUIContent("Next Dialogue - Succeed"));
                            EditorGUILayout.PropertyField(failedNextDialogue, new GUIContent("Next Dialogue - Failed"));

                            break;

                        case 5:

                            EditorGUILayout.PropertyField(successNextDialogue,
                                new GUIContent("Next Dialogue - Succeed"));
                            EditorGUILayout.PropertyField(failedNextDialogue, new GUIContent("Next Dialogue - Failed"));

                            break;
                    }

                    EditorGUILayout.EndFoldoutHeaderGroup();
                }

                break;
        }
    }*/
}