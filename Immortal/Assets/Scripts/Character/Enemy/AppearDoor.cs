using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearDoor : MonoBehaviour {

    Vector3 origin;
    GameObject door;

	// Use this for initialization
	void Start () {
        door = (GameObject)Resources.Load("Prefabs/Door");
        origin = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.childCount == 0)
        {
            Instantiate(door, origin, Quaternion.identity);
            Destroy(this);
        }
	}
}
