using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour {

    Rooms2 room;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("1");
        if (other.tag == "Player")
        {
            room.state++;

            room.ChangeRoom2();
            Destroy(this.gameObject, 3);
        }
    }

    // Use this for initialization
    void Start () {
        room = GameObject.Find("Rooms").GetComponent<Rooms2>();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
