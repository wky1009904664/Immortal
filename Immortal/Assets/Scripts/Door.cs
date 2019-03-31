using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    RoomController room;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            room.state++;
           
            room.ChangeRoom();
            Debug.Log("1");
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        room = GameObject.Find("RoomController").GetComponent<RoomController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
