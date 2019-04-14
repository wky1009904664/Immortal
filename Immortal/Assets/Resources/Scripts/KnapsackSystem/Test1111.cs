using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1111 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.G))
        {
            int rand = Random.Range(1, 4);
            Inventory.GetInstance.StoreGood(rand);
        }

        if (Input.GetKeyDown(KeyCode.T))
            Inventory.GetInstance.ShowPanel();
        if (Input.GetKeyDown(KeyCode.R))
            Inventory.GetInstance.HidePanel();
	}
}
