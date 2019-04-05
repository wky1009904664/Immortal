using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;


public class DeleteDoor : MonoBehaviour
{
    [MenuItem("Tools/ DeleteDoor")]
    public static void CreatPrefab()
    {

        Transform[] aaaa = Selection.transforms;    //   只能在 Hierarchy 面板下多选，在其他面板 下 只能 选一个
        Debug.Log(aaaa.Length);
        if (aaaa.Length == 0)
        {
            return;
        }

        Debug.Log(Application.dataPath + " lujing");

        for (int i = 0; i < aaaa.Length; i++)
        {
            Transform f1 = aaaa[i].GetChild(0);
           
        }
    }
}
