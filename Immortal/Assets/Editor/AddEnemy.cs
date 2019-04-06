using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class AddEnemy 
{
    [MenuItem("Tools/ AddEnemy")]
    public static void CreatPrefab()
    {

        Transform[] aaaa = Selection.transforms;    //   只能在 Hierarchy 面板下多选，在其他面板 下 只能 选一个
        if (aaaa.Length == 0)
        {
            return;
        }
        

        for (int i = 0; i < aaaa.Length; i++)
        {
            GameObject enemys = new GameObject("Enemys");
            enemys.transform.parent = aaaa[i];

          
        }
    }
}
