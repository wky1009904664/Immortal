using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRen : MonoBehaviour {

    LineRenderer gunLine;
    Ray shootRay;
    RaycastHit shootHit;

    // Use this for initialization
    void Start () {
        gunLine = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        shoot();
	}

    void shoot()
    {

        gunLine.SetPosition(0, transform.position - new Vector3(0, 2, 0));
        shootRay.origin = transform.position - new Vector3(0, 2, 0);
        shootRay.direction = transform.forward;
        if (Physics.Raycast(shootRay, out shootHit, 100))
        {
            PlayerMovement player = shootHit.collider.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.DecreaseHealth();
                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                if(shootHit.collider.tag=="Wall")
                    gunLine.SetPosition(1, shootHit.point);
            }
        }
    }
}
