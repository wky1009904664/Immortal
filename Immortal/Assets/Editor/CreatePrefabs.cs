using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class CreatePrefabs
{
    [MenuItem("Tools/ CreatPrefab")]
    public static void CreatPrefab()
    {

        Transform[] aaaa = Selection.transforms;    //   只能在 Hierarchy 面板下多选，在其他面板 下 只能 选一个
        if (aaaa.Length == 0)
        {
            return;
        }
        // UnityEngine.Object tempPrefab;

        //if (aaaa[0].transform.GetComponent<TaKeUp>())
        //{
        //    Debug.Log("aaaaaaaa");

        //}

        for (int i = 0; i < aaaa.Length; i++)
        {
            

           // PrefabUtility.CreatePrefab("Assets/Resources/Prefabs/Rooms/" + aaaa[i].name + ".prefab" +
           //     "", aaaa[i].gameObject);
        
        }
    }
}
