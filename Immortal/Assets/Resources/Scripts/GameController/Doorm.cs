using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorm : MonoBehaviour
{

    private bool canAccess = false;
    public Vector3 playerTarget;
    public int from;
    public int to;
    public int Floor;
    Vector3[] loca;
    GameObject barrier;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        switch (Floor)
        {
            case 1:loca = F1Creater.Location;
                break;
            case 2:loca = F2Creater.Location;
                break;
            case 3:loca = F3Creater.Location;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            OpenDoor();
    }

    public void ChangeRoom()
    {

    }

    public void OpenDoor()
    {
        canAccess = true;
        barrier = this.transform.GetChild(1).gameObject;
        Destroy(barrier);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (canAccess)
            {
                player.position = loca[to - 1];
                Camera2.dire = (loca[to - 1] - loca[from - 1]).normalized;
                Debug.Log(Camera2.dire);
                Camera.main.GetComponent<Camera2>().speed = 0;
                Camera.main.GetComponent<Camera2>().dec = false;
                Camera.main.GetComponent<Camera2>().Move1(Camera2.dire);
            }
        }
    }
}
