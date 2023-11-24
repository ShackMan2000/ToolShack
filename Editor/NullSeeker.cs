using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[ExecuteInEditMode]
public class NullSeeker : MonoBehaviour
{
    // to do :
    // check prefabs
    // check scriptable objects
    // check nested classes and structs

    // later: auto select button
    // nicer layout with custom editor


    [SerializeField] bool refreshOnScriptSave;

    public List<NullObject> SceneObjects;
    public List<NullObject> Prefabs;
    public List<NullObject> ScriptableObjects;

    [SerializeField] int maxObjectsChecked = 1000;


    public bool checkSceneObjects;
    public bool checkPrefabs;
    public bool checkScriptableObjects;

    // void OnValidate()
    // {
    //     if (checkOnReload)
    //     {
    //         CheckSceneObjects();
    //     }
    // }


    public void ReactToScriptSave()
    {
        if (checkSceneObjects)
        {
            CheckSceneObjects();
        }

        if (checkPrefabs)
        {
            CheckPrefabs();
        }
        
        
    }

    [Serializable]
    public class NullObject
    {
        [SerializeField] GameObject go;

        public List<ComponentWithNullFields> components;

        public NullObject(GameObject go)
        {
            this.go = go;
            components = new List<ComponentWithNullFields>();
        }
    }


    [Serializable]
    public class ComponentWithNullFields
    {
        public Component component;

        // can't serialize FieldInfo
        public List<FieldInfo> nullFields;
        public List<string> nullFieldNames;

        public ComponentWithNullFields(Component component)
        {
            this.component = component;
            nullFields = new List<FieldInfo>();
            nullFieldNames = new List<string>();
        }

        public void AddField(FieldInfo field)
        {
            nullFields.Add(field);
            nullFieldNames.Add(field.Name);
        }
    }


    [Button]
    public void CheckSceneObjects()
    {
        //  objectsWithNullFields = new Dictionary<GameObject, List<FieldInfo>>();

        int count = 0;


        SceneObjects = new List<NullObject>();

        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();

//        Debug.Log("Found " + objects.Length + " objects");

        foreach (GameObject obj in objects)
        {
            count++;
            if (count > maxObjectsChecked)
            {
                break;
            }


            NullObject nullObject = new NullObject(obj);


            Component[] comps = obj.GetComponents(typeof(MonoBehaviour));
            List<Component> filteredComps = new List<Component>();

            foreach (Component comp in comps)
            {
                Type monoType = comp.GetType();


                string typeNamespace = monoType.Namespace;

                if (typeNamespace != null)
                {
                    if (typeNamespace == "UnityEngine" || typeNamespace.StartsWith("UnityEngine" + ".") || typeNamespace == "TMPro")
                    {
                        continue;
                    }
                }

                filteredComps.Add(comp);
            }


            foreach (Component comp in filteredComps)
            {
                ComponentWithNullFields componentWithNullFields = new ComponentWithNullFields(comp);


                FieldInfo[] fieldsPublic = comp.GetType().GetFields(
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);


                foreach (FieldInfo field in fieldsPublic)
                {
                    if (field.IsPrivate && !field.IsDefined(typeof(SerializeField), false))
                    {
                        continue;
                    }

                    object value = field.GetValue(comp);

                    if (value == null || value.Equals(null))
                    {
                        //      Debug.Log("Found null field " + field.Name + " in " + comp.GetType().Name);
                        componentWithNullFields.AddField(field);
                    }
                }

                //
                // Debug.Log("Found " + componentWithNullFields.nullFields.Count + " null fields in " + comp.GetType().Name);
                if (componentWithNullFields.nullFields.Count > 0)
                {
                    nullObject.components.Add(componentWithNullFields);
                }
            }


            if (nullObject.components.Count > 0)
            {
//                Debug.Log("Found " + nullObject.components.Count + " components with null fields in " + obj.name);
                SceneObjects.Add(nullObject);
            }
        }


        // print dictionary
        // foreach (var pair in objectsWithNullFields)
        // {
        //     Debug.Log(pair.Key.name + " has " + pair.Value.Count + " null fields");
        //
        //     foreach (var field in pair.Value)
        //     {
        //         Debug.Log(field.Name);
        //     }
        //
        //     Debug.Log(" ");
        // }
    }


    [Button]
    public void CheckPrefabs()
    {
        //string[] pr = AssetDatabase.FindAssets("t: prefab", new[] {"Assets/Prefabs"});


        string folderPath = "Assets/Prefabs";


        // Get all prefab files in the folder
        string[] prefabFiles = Directory.GetFiles(folderPath, "*.prefab");


        // Get all subdirectories in the current folder
        string[] subdirectories = Directory.GetDirectories(folderPath);

        // Recursively search for prefabs in subfolders
        foreach (string subdirectory in subdirectories)
        {
        }

        Debug.Log("Found " + prefabFiles.Length + " prefabs");

        // Loop through each prefab file
        foreach (string prefabFile in prefabFiles)
        {
            // Load the prefab as a GameObject
            GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath(prefabFile, typeof(GameObject));

            if (prefab != null)
            {
                // Get all components attached to the prefab
                Component[] components = prefab.GetComponents<Component>();

                // Loop through each component and do something with it
                foreach (Component component in components)
                {
                    // Example: Print the component's name to the console
                    //Debug.Log("Component nName: " + component.GetType().Name);
                }
            }
        }
    }


// }      // Get the path to the folder containing your prefabs
// string folderPath = "Assets/" + prefabFolderName;
//
// // Check if the folder exists
// if (Directory.Exists(folderPath))
// {
//     // Search for prefabs in the specified folder and its subfolders
//     SearchForPrefabs(folderPath);
// }
// else
// {
//     Debug.LogError("Prefab folder not found: " + folderPath);
// }
// }
//
// private void SearchForPrefabs(string folder)
// {
//     // Get all prefab files in the current folder
//     string[] prefabFiles = Directory.GetFiles(folder, "*.prefab");
//
//     // Loop through each prefab file in the current folder
//     foreach (string prefabFile in prefabFiles)
//     {
//         // Load the prefab as a GameObject
//         GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath(prefabFile, typeof(GameObject));
//
//         if (prefab != null)
//         {
//             // Get all components attached to the prefab
//             Component[] components = prefab.GetComponents<Component>();
//
//             // Loop through each component and do something with it
//             foreach (Component component in components)
//             {
//                 // Example: Print the component's name to the console
//                 Debug.Log("Component Name: " + component.GetType().Name);
//             }
//         }
//     }
//
//     // Get all subdirectories in the current folder
//     string[] subdirectories = Directory.GetDirectories(folder);
//
//     // Recursively search for prefabs in subfolders
//     foreach (string subdirectory in subdirectories)
//     {
//         SearchForPrefabs(subdirectory);
//     }
}