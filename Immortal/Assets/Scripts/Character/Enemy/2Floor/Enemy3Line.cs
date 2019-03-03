using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Line : MonoBehaviour {

    LineRenderer gunLine;
    LineRenderer gunLine2;
    Ray shootRay;
    RaycastHit shootHit;
    Transform target;
    float timeval = 0;
    float Attackcd = 0.5f;
    float attackTimeval = 0;

    float alertTime = 1.5f;
    float shotcd = 5.0f;
    float locktime =4;

    // Use this for initialization
    void Start () {
        target = this.transform.GetChild(0).transform;
        gunLine2 = this.transform.GetChild(1).GetComponent<LineRenderer>();
        gunLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update () {
        timeval += Time.deltaTime;
        attackTimeval += Time.deltaTime;
        if (timeval <= alertTime-0.5f && timeval >= 0)
        {
            gunLine2.enabled = true;
            LineAlert();
        }
        else if (timeval <= alertTime)
        {
            gunLine2.enabled = false;
        }
        else if (timeval >= 0 && timeval <= locktime)
        {
            gunLine2.enabled = false;
            gunLine.enabled = true;
            LineShot();
        }
        else if (timeval <= shotcd && timeval >= 0)
        {
            gunLine.enabled = false;
        }
        else if (timeval >= shotcd)
        {
            timeval = 0;
            target.GetComponent<Enemy3Nav>().CHangePosition();
        }
	}

    void LineShot()
    {
        Vector3 dis = target.transform.position - this.transform.position;
        gunLine.SetPosition(0, transform.position);
        shootRay.origin = transform.position;
        shootRay.direction = dis.normalized;
        if(Physics.Raycast(shootRay,out shootHit, 100))
        {
            PlayerMovement player = shootHit.collider.GetComponent<PlayerMovement>();
            if (player != null)
            {
                if (attackTimeval >= Attackcd)
                {
                    player.DecreaseHealth();
                    attackTimeval = 0;
                }
                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                gunLine.SetPosition(1, shootHit.point);
            }
        }
    }

    void LineAlert()
    {

        Vector3 dis = target.transform.position - this.transform.position;
        gunLine2.SetPosition(0, transform.position);
        shootRay.origin = transform.position;
        shootRay.direction = dis.normalized;
        if (Physics.Raycast(shootRay, out shootHit, 100))
        {
            PlayerMovement player = shootHit.collider.GetComponent<PlayerMovement>();
            if (player != null)
            {
                gunLine2.SetPosition(1, shootHit.point);
            }
            else
            {
                gunLine2.SetPosition(1, shootHit.point);
            }
        }
    }
}
