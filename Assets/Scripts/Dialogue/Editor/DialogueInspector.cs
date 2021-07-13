using System;
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
    private GUIStyle _headerGuiStyle;
    private GUIStyle _branchHeaderStyle;
    private List<Color> _headerColors;

    private void OnEnable()
    {
        _type = serializedObject.FindProperty("type");
        _characterName = serializedObject.FindProperty("characterName");
        _sentence = serializedObject.FindProperty("sentence");
        
        _branchHeaderStyle = new GUIStyle();
        _branchHeaderStyle.normal.textColor = Color.white;
        _branchHeaderStyle.fontSize = 12;
        _branchHeaderStyle.fontStyle = FontStyle.Bold;
        _headerGuiStyle = new GUIStyle();
        _headerGuiStyle.fontSize = 16;
        _headerGuiStyle.normal.textColor = Color.yellow;
        _headerGuiStyle.fontStyle = FontStyle.Bold;
        _headerGuiStyle.alignment = TextAnchor.MiddleCenter;
    }
    

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //EditorGUILayout.Space();
        //EditorGUILayout.LabelField("Dialogue Editor", _headerGuiStyle);
        //EditorGUILayout.Space();
        //EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(_type, new GUIContent("Dialogue Type"));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(_characterName, new GUIContent("Character Name Displayed"));
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Sentence");
        EditorGUILayout.PropertyField(_sentence, new GUIContent(), GUILayout.Height(90));
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
                SerializedProperty arraySizeProp = choices.FindPropertyRelative("Array.size");
                EditorGUILayout.PropertyField(arraySizeProp, new GUIContent("Number of options"));

                for (int i = 0; i < choices.arraySize; i++)
                {
                    SerializedProperty branchText = choices.GetArrayElementAtIndex(i).FindPropertyRelative("choiceText");
                    SerializedProperty actionType = choices.GetArrayElementAtIndex(i).FindPropertyRelative("action");
                    SerializedProperty successNextDialogue01 = choices.GetArrayElementAtIndex(i).FindPropertyRelative("successDialogue01");
                    SerializedProperty successNextDialogue02 = choices.GetArrayElementAtIndex(i).FindPropertyRelative("successDialogue02");
                    SerializedProperty failedNextDialogue = choices.GetArrayElementAtIndex(i).FindPropertyRelative("failedDialogue");
                    SerializedProperty failedNoDrugsNextDialogue = choices.GetArrayElementAtIndex(i).FindPropertyRelative("failedNoDrugsDialogue");
                    SerializedProperty failedNoMoneyNextDialogue = choices.GetArrayElementAtIndex(i).FindPropertyRelative("failedNoMoneyDialogue");
                    SerializedProperty failedNoPistolNextDialogue = choices.GetArrayElementAtIndex(i).FindPropertyRelative("failedNoWeaponDialogue");
                    SerializedProperty nextDialogue = choices.GetArrayElementAtIndex(i).FindPropertyRelative("nextDialogue");
                    
                    EditorGUILayout.Space(10);
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                    EditorGUILayout.Space(10);
                    EditorGUILayout.LabelField(branchText.stringValue, _branchHeaderStyle);
                    EditorGUILayout.Space(10);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(branchText, new GUIContent("Option Text"));
                    EditorGUILayout.Space(10);
                    EditorGUILayout.PropertyField(actionType, new GUIContent("Action Type"));
                    Choice.ActionType _actionType = (Choice.ActionType) actionType.enumValueIndex;
                    

                    switch (_actionType)
                    {
                        case Choice.ActionType.Generic:
                            
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(nextDialogue, new GUIContent());
                            
                            break;

                        case Choice.ActionType.BuyDrugs:

                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - Succeed");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(successNextDialogue01, new GUIContent());
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - No Money Failed");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(failedNoMoneyNextDialogue, new GUIContent());

                            break;

                        case Choice.ActionType.SellDrugs:
                            
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - Succeed");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(successNextDialogue01, new GUIContent());
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - No Drugs Failed");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(failedNoDrugsNextDialogue, new GUIContent());
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - Failed");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(failedNextDialogue, new GUIContent());
                            
                            break;

                        case Choice.ActionType.BuyWeapon:

                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - Succeed");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(successNextDialogue01, new GUIContent());
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - No Money Failed");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(failedNoMoneyNextDialogue, new GUIContent());
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - Failed Already Had A Weapon");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(failedNextDialogue, new GUIContent());

                            break;

                        case Choice.ActionType.Steal:

                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - Succeed - First Time");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(successNextDialogue01, new GUIContent());
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - Succeed - Second Time");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(successNextDialogue02, new GUIContent());
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - No Weapon Failed");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(failedNoPistolNextDialogue, new GUIContent());

                            break;

                        case Choice.ActionType.Rape:
                            
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - Succeed - First Time");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(successNextDialogue01, new GUIContent());
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - Succeed - Second Time");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(successNextDialogue02, new GUIContent());
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - No Weapon Failed");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(failedNoPistolNextDialogue, new GUIContent());

                            break;
                        
                        case Choice.ActionType.BuyDrink:

                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - Succeed");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(successNextDialogue01, new GUIContent());
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - No Money Failed");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(failedNoMoneyNextDialogue, new GUIContent());

                            break;
                        
                        case Choice.ActionType.GetRobbed:

                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - Robbed with drugs");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(nextDialogue, new GUIContent());
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - Robbed with no drugs");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(failedNoDrugsNextDialogue, new GUIContent());

                            break;
                        
                        case Choice.ActionType.GoBrothel:
                            
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(nextDialogue, new GUIContent());
                            
                            break;
                        
                        case Choice.ActionType.GetOral:
                            
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(nextDialogue, new GUIContent());
                            
                            break;
                        
                        case Choice.ActionType.GetVaginal:
                            
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(nextDialogue, new GUIContent());
                            
                            break;
                        
                        case Choice.ActionType.GetAnal:
                            
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(nextDialogue, new GUIContent());
                            
                            break;
                        
                        case Choice.ActionType.GameOver:
                            
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(nextDialogue, new GUIContent());
                            
                            break;
                        
                        case Choice.ActionType.BuyParadise:

                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - Succeed");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(successNextDialogue01, new GUIContent());
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - No Money Failed");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(failedNoMoneyNextDialogue, new GUIContent());

                            break;
                        
                        case Choice.ActionType.BuyRaspadinha:
                            
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - Succeed");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(successNextDialogue01, new GUIContent());
                            EditorGUILayout.Space(10);
                            EditorGUILayout.LabelField("Next Dialogue - No Money Failed");
                            EditorGUILayout.Space();
                            EditorGUILayout.PropertyField(failedNoMoneyNextDialogue, new GUIContent());

                            break;
                    }

                    EditorGUILayout.EndFoldoutHeaderGroup();
                    EditorGUI.indentLevel--;
                    EditorGUILayout.Separator();
                }

                break;
        }
    
        serializedObject.ApplyModifiedProperties();
    }

}