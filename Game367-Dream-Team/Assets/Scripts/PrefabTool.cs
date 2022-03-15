using System.Collections;
using UnityEngine;
using UnityEditor;

public class PrefabTool : EditorWindow
{
    bool rigid;
    int mass;
    float scaleSize;
    bool audio;
    bool playOnAwake;
    string objName;
    GameObject[] gObj;

    [MenuItem("GAME 367/Prefab Tool")]
    public static void ShowWindow()
    {
        GetWindow<PrefabTool>("Prefab Tool");
    }
    void OnGUI()
    {
        GUILayout.Label("Elements to Add", EditorStyles.boldLabel);
        GUILayout.Space(5);
        GUILayout.Label("Name to be added to object", EditorStyles.boldLabel);
        objName = EditorGUILayout.TextField("Name", objName);
        rigid = EditorGUILayout.Toggle("Add Rigidbody", rigid);
        GUILayout.Space(20);
        GUILayout.Label("How heavy is the objects?", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Small"))
        {
            mass = 5;
            scaleSize = 2;
        }
        else if (GUILayout.Button("Medium"))
        {
            mass = 15;
            scaleSize = 5;
        }
        else if (GUILayout.Button("Large"))
        {
            mass = 40;
            scaleSize = 10;
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        GUILayout.Label("Audio Components", EditorStyles.boldLabel);
        audio = EditorGUILayout.Toggle("Add Audio Source", audio);
        playOnAwake = EditorGUILayout.Toggle("Play on Awake", playOnAwake);
        if (GUILayout.Button("Finished"))
        {
            foreach(GameObject obj in Selection.gameObjects)
            {
                gObj = Selection.gameObjects;
                gObj[0].gameObject.name = objName;
                if(rigid == true)
                {
                    gObj[0].AddComponent<Rigidbody>();
                    switch (mass)
                    {
                        case 5:
                            gObj[0].GetComponent<Rigidbody>().mass = 5;
                            Vector3 newSizeSmall = new Vector3(scaleSize, scaleSize, scaleSize);
                            gObj[0].gameObject.transform.localScale = newSizeSmall;
                            break;
                        case 15:
                            gObj[0].GetComponent<Rigidbody>().mass = 15;
                            Vector3 newSizeMedium = new Vector3(scaleSize, scaleSize, scaleSize);
                            gObj[0].gameObject.transform.localScale = newSizeMedium;
                            break;
                        case 40:
                            gObj[0].GetComponent<Rigidbody>().mass = 40;
                            Vector3 newSizeLarge = new Vector3(scaleSize, scaleSize, scaleSize);
                            gObj[0].gameObject.transform.localScale = newSizeLarge;
                            break;
                        default:
                            Debug.LogError("You forgot a size dummy");
                            gObj[0].GetComponent<Rigidbody>().mass = 1;
                            break;
                    }
                }
                else
                {
                    return;
                }
                if(audio == true)
                {
                    gObj[0].AddComponent<AudioSource>();
                    if(playOnAwake == true)
                    {
                        gObj[0].GetComponent<AudioSource>().playOnAwake = true;
                    }
                    else
                    {
                        gObj[0].GetComponent<AudioSource>().playOnAwake = false;
                    }
                }
                else
                {
                    return;
                }
            }
        }
        if (GUILayout.Button("Reset"))
        {
            Vector3 resetSize = new Vector3(1, 1, 1);
            gObj[0].gameObject.transform.localScale = resetSize;
            if(rigid == true)
            {
                DestroyImmediate(gObj[0].GetComponent<Rigidbody>());
            }
            else
            {
                return;
            }
            if (audio == true)
            {
                DestroyImmediate(gObj[0].GetComponent<AudioSource>());
            }
            else
            {
                return;
            }
        }
    }
}
