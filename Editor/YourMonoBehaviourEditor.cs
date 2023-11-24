// using UnityEditor;
// using UnityEngine;
//
// [CustomEditor(typeof(NullSeeker))]
// public class YourMonoBehaviourEditor : Editor
// {



    // private SerializedProperty nullObjectsProperty;
    //
    //
    //
    // //tinte editor
    // private void OnEnable()
    // {
    //     Debug.Log("OnEnable");
    //     nullObjectsProperty = serializedObject.FindProperty("nullObjects");
    //     Debug.Log("nullObjectsProperty = " + nullObjectsProperty);
    // }
    //
    // public override void OnInspectorGUI()
    // {
    //     serializedObject.Update();
    //
    //     
    //    // EditorGUILayout.PropertyField(nullObjectsProperty, true);
    //
    //     if (nullObjectsProperty.isExpanded)
    //     {
    //         EditorGUI.indentLevel++;
    //
    //         for (int i = 0; i < nullObjectsProperty.arraySize; i++)
    //         {
    //             SerializedProperty nullObjectProperty = nullObjectsProperty.GetArrayElementAtIndex(i);
    //             
    //             SerializedProperty gameObjectWithNull = nullObjectProperty.FindPropertyRelative("go");
    //             SerializedProperty components = nullObjectProperty.FindPropertyRelative("components");
    //
    //             EditorGUILayout.PropertyField(gameObjectWithNull, true);
    //
    //             if (components.isExpanded)
    //             {
    //                 EditorGUI.indentLevel++;
    //
    //                 for (int j = 0; j < components.arraySize; j++)
    //                 {
    //                     SerializedProperty comp = components.GetArrayElementAtIndex(j);
    //
    //                     SerializedProperty nullFieldsProperty = comp.FindPropertyRelative("nullFieldNames");
    //
    //                     EditorGUILayout.PropertyField(comp, true);
    //
    //                     if (nullFieldsProperty.isExpanded)
    //                     {
    //                         EditorGUI.indentLevel++;
    //
    //                         for (int k = 0; k < nullFieldsProperty.arraySize; k++)
    //                         {
    //                             SerializedProperty nullFieldProperty = nullFieldsProperty.GetArrayElementAtIndex(k);
    //                          //   EditorGUILayout.PropertyField(nullFieldProperty, true);
    //                         }
    //
    //                         EditorGUI.indentLevel--;
    //                     }
    //                 }
    //
    //                 EditorGUI.indentLevel--;
    //             }
    //         }
    //
    //         EditorGUI.indentLevel--;
    //     }
    //     
    //     
    //     
    //     if (GUILayout.Button("Get Data"))
    //     {
    //         NullSeeker targetScript = (NullSeeker)target;
    //         targetScript.GetNullFields();
    //     }
    //
    //
    //     serializedObject.ApplyModifiedProperties();
   // }
//}
