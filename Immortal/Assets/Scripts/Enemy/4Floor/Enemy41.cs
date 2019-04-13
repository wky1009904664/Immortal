using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy41 : MonoBehaviour
{

    float timeval = 1f;
    public int Health = 100;
    public float rotspeed = 2;//旋转角速度
    GameObject DieEffect;
    Transform dieEffect;
    public Transform target;
    public float diespeed = 100;
    // Use this for initialization
    void Start()
    {
        DieEffect = (GameObject)Resources.Load("DiedEffect");
    }

    // Update is called once per frame
    void Update()
    {
        timeval += Time.deltaTime;
        if (Health <= 0)
            Die();
        transform.RotateAround(new Vector3(0, 0, -12.5f), new Vector3(0, 1, 0), rotspeed);
    }

    public void DecreaseHealth()
    {
        Health -= 20;
    }

    void Die()
    {
        dieEffect = Instantiate(DieEffect, this.transform.position, Quaternion.identity).transform;
        dieEffect.LookAt(target);
        dieEffect.Rotate(new Vector3(0, 0, -90), Space.Self);
        dieEffect.GetComponent<Rigidbody>().velocity = dieEffect.transform.forward.normalized * diespeed;
        Destroy(this.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (timeval >= 0.5f)
            {
                other.GetComponent<PlayerMovement>().DecreaseHealth();
                timeval = 0;
            }
        }
    }
}
