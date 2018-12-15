using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Range(0, 10)]
    public float AlertRadius;

    [Range(0, 360)]
    public float Alertangle;

    public int shotAmount;
    public float Attackcd;
    public float bulletSpeed;

    bool AlertIsTrue;
    Transform player;
    GameObject bullet;
    Rigidbody bulletrigi;
    float timeval = 0;

    void Alert()
    {
        Vector3 dis = player.position - this.transform.position;
        float distance = dis.magnitude;
        float disAngle = Vector3.Angle(dis, this.transform.forward);
        if (distance <= AlertRadius && disAngle <= Alertangle)
        {
            AlertIsTrue = true;
        }
        else
        {
            AlertIsTrue = false;
        }
    }

    void Attack()
    {
        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;
        for (int i = 0; i < shotAmount; i++)
        {
            bulletrigi = Instantiate(bullet, this.transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity).GetComponent<Rigidbody>();
            direction = Quaternion.Euler(0, 360 / shotAmount, 0) * direction;
            bulletrigi.AddForce(direction.normalized * bulletSpeed);
        }

        timeval = 0;
    }

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bullet = (GameObject)Resources.Load("Prefabs/EnemyBullet");
    }

    // Update is called once per frame
    void Update()
    {
        Alert();
        timeval += Time.deltaTime;
        if (timeval >= Attackcd)
        {
            if (AlertIsTrue)
                Attack();
        }
    }
}
