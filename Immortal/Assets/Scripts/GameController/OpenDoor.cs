﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Transform Doors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckAmount()
    {
        if (transform.childCount == 1)
        {
            Doors = transform.parent.GetChild(3);
            foreach(Transform child in Doors)
            {
                child.GetComponent<Doorm>().OpenDoor();
            }
        }
    }
}
