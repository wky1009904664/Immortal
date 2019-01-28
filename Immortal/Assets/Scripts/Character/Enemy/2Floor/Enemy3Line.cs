using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Line : MonoBehaviour {

    LineRenderer gunLine;
    Ray shootRay;
    RaycastHit shootHit;
    Transform target;
    public float timeval = 0;
    float Attackcd = 0.5f;
    float attackTimeval = 0;
    

    public float shotcd;
    public float locktime;

    // Use this for initialization
    void Start () {
        target = this.transform.GetChild(0).transform;
        gunLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update () {
        timeval += Time.deltaTime;
        attackTimeval += Time.deltaTime;
        if (timeval <= locktime && timeval >= 0)
        {
            LineShot();
        }
        else if (timeval <= shotcd && timeval >= 0)
        {
            gunLine.enabled = false;
        }
        else if (timeval >= shotcd)
        {
            timeval = 0;
            gunLine.enabled = true;
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
}
