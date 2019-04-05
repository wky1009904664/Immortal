﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F1Creater : MonoBehaviour
{
    Vector3[] Location = { new Vector3(0, 0, 0), new Vector3(19, 0, 0), new Vector3(9.3f, 0, -16.5f), new Vector3(-9.3f, 0, -16.5f), new Vector3(0,0,-33.2f), new Vector3(19,0,-33.2f), new Vector3(-9.3f,0,-49.25f), new Vector3(9.3f,0,-49.3f)};
    Quaternion angle = Quaternion.Euler(0, 30, 0);
    int ord;
    string path;
    GameObject room;
    GameObject  f1;
    Transform f2;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 8; i++)
        {
            ord = Random.Range(0, 25);
            path = string.Format("Prefabs/Rooms/Room1 ({0})", ord); 
            Debug.Log(path);
            room = (GameObject)Resources.Load(path);
            f1=Instantiate(room, Location[i], angle);
            f2 = f1.transform.GetChild(0);
            f2 = f2.transform.GetChild(3);
            Destroy(f2.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
