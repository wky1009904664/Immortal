using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {

    public int state = 0;
    private Transform player;
    GameObject room2;
    GameObject room3;
    GameObject room4;
    GameObject room5;
    GameObject bossroom;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<Transform>();
        room2 = this.transform.Find("room2").gameObject;
        room3 = this.transform.Find("room3").gameObject;
        room4 = this.transform.Find("room4").gameObject;
        room5 = this.transform.Find("room5").gameObject;
        bossroom = this.transform.Find("Bossroom").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
      
	}



    public void ChangeRoom()
    {
        Camera.main.GetComponent<CameraMove>().speed = 1;
        Camera.main.GetComponent<CameraMove>().dec = false;
        Camera.main.GetComponent<CameraMove>().Move1();
        switch (state)
        {
            case 1:
                room4.SetActive(true);
                room3.SetActive(false);
                player.position = new Vector3(110, 1.03f, -20);
                break;
            case 2:
                room5.SetActive(true);
                room4.SetActive(false);
                player.position = new Vector3(170, 1.03f, -20);
                break;
            case 3:
                bossroom.SetActive(true);
                room5.SetActive(false);
                player.position = new Vector3(220, 1.03f, -20);
                break;
            case 4:
                room2.SetActive(true);
                bossroom.SetActive(false);
                player.position = new Vector3(261, 1.03f, -20);
                break;
    
        }
    }

}
