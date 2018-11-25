using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGood : MonoBehaviour {

    public int ID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Inventory.GetInstance.StoreGood(ID);
            Destroy(gameObject);
        }
    }
}
