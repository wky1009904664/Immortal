using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{

    public int state = 0;
    public int shotAmount;
    public float Attackcd;
    public float bulletSpeed;
    public float statetime = 6.0f;
    public float rotaSpeed = 10;
    public int height;
    float attackval = 2.0f;
    float timeval2;
    float timeval;
    GameObject bullet;
    Rigidbody rigi;
    Rigidbody boss;
    int health = 500;
    Vector3 leftup = new Vector3(-12, 0, 7);
    Vector3 leftbot = new Vector3(-12, 0, -7);
    Vector3 rightup = new Vector3(12, 0, 7);
    Vector3 rightbot = new Vector3(12, 0, -7);
    float distance = new Vector3(12, 0, 7).magnitude;

    LineRenderer gunLine;
    Ray shootRay;
    RaycastHit shootHit;
    // Use this for initialization
    void Start()
    {
        bullet = (GameObject)Resources.Load("Prefabs/bounceBullet");
        boss = this.GetComponent<Rigidbody>();
       // gunLine = GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            Die();
                this.transform.Rotate(new Vector3(0, 1, 0), rotaSpeed * Time.deltaTime);
        switch (state)
        {
            case 0://旋转X + 圆形弹幕
                attackval += Time.deltaTime;
                timeval += Time.deltaTime;
               // var child = this.GetComponentsInChildren<LineRen>();
                foreach(Transform child in this.transform)
                {
                    child.GetComponent<LineRenderer>().enabled = true;
                }
                Attack1();
                if (timeval >= statetime)
                {
                    state = 1;
                    timeval = 0;
                    timeval2=0;
                    attackval = 0;
                    foreach (Transform child in this.transform)
                    {
                        child.GetComponent<LineRenderer>().enabled = false;
                    }
                }
                break;
            case 1://跳跃
                timeval2 += Time.deltaTime;
                attackval += Time.deltaTime;
                if (timeval2 < 1.0f)
                    Jumpto(leftup);
                else if (timeval2 < 2.0f)
                    Jumpto(leftbot - leftup);
                else if (timeval2 < 3.0f)
                    Jumpto(-leftbot);
                else if (timeval2 < 4.0f)
                    Jumpto(rightup);
                else if (timeval2 < 5.0f)
                    Jumpto(rightbot - rightup);
                else if (timeval2 < 6.0f)
                    Jumpto(-rightbot);
                else
                {
                    timeval2 = 0;
                    state = 2;
                }
                if (attackval >= 0.99f)
                {
                    boss.AddForce(0, height, 0);
                    Attack2();
                    attackval = 0;
                }
                break;
            case 2://
                timeval += Time.deltaTime;
                attackval += Time.deltaTime;
                Attack1();
                if (timeval >= statetime)
                {
                    state = 0;
                    timeval = 0;
                    timeval2 = 0;
                    attackval = Attackcd;
                }
                break;
        }
    }

    void Attack1()
    {
        if (attackval >= Attackcd)
        {
            Vector3 direction = this.transform.forward;
            for (int i = 0; i < shotAmount; i++)
            {
                direction = Quaternion.Euler(0, 360 / shotAmount, 0) * direction;
                rigi = Instantiate(bullet, this.transform.position + direction.normalized * 0.1f + new Vector3(0, -2.0f, 0), Quaternion.identity).GetComponent<Rigidbody>();
                rigi.AddForce(direction.normalized * bulletSpeed);
            }
            attackval = 0;
        }
    }

    void shoot()
    {
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position - new Vector3(0, 2, 0));
        shootRay.origin = transform.position - new Vector3(0, 2, 0);
        shootRay.direction = transform.forward;
        if(Physics.Raycast(shootRay,out shootHit, 100))
        {
            PlayerMovement player = shootHit.collider.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.DecreaseHealth();
                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * 100);
            }
        }
    }

    void Attack2(int amount = 5)
    {
        Vector3 direction = this.transform.forward;
        for (int i = 0; i < 5; i++)
        {
            direction = Quaternion.Euler(0, 360 / 5, 0) * direction;
            rigi = Instantiate(bullet, this.transform.position + direction.normalized * 0.1f + new Vector3(0, -2.0f, 0), Quaternion.identity).GetComponent<Rigidbody>();
            rigi.AddForce(direction.normalized * bulletSpeed);
        }
    }



    void Die()
    {
        Destroy(this.gameObject);
    }

    public void DecreaseHealth()
    {
        health -= 20;
    }

    void Jumpto(Vector3 target)
    {
        this.transform.Translate(target * Time.deltaTime, Space.World);
    }
}
