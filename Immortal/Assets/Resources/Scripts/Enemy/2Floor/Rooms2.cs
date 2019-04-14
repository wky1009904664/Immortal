using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooms2 : MonoBehaviour {

    public int state = 0;
    private Transform player;
    GameObject room1;
    GameObject room2;
    GameObject room3;
    GameObject bossroom;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<Transform>();
        room1 = this.transform.Find("room1").gameObject;
        room2 = this.transform.Find("room2").gameObject;
        room3 = this.transform.Find("room3").gameObject;
        bossroom = this.transform.Find("Bossroom").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeRoom2()
    {
        Camera.main.GetComponent<CameraMove>().speed = 1;
        Camera.main.GetComponent<CameraMove>().dec = false;
        Camera.main.GetComponent<CameraMove>().Move1();
        switch (state)
        {
            case 1:
                room2.SetActive(true);
                room1.SetActive(false);
                player.position = new Vector3(63, 1.03f, -16.3f);
                break;
            case 2:
                room3.SetActive(true);
                room2.SetActive(false);
                player.position = new Vector3(120, 1.03f, -16.3f);
                break;
            case 3:
                bossroom.SetActive(true);
                room3.SetActive(false);
                player.position = new Vector3(170, 1.03f, -16.3f);
                break;
        }
    }

}
