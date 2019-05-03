using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss1 : MonoBehaviour
{
    //前摇-寻路-散射-前摇-寻路-散射-回中心-激光
    public int state = 0;
    public int shotAmount;
    Transform player;
    public float Attackcd;
    private float bulletSpeed = 5;
    public float statetime = 6.0f;
    public float rotaSpeed = 10;
    public int height;
    float attackval = 2.0f;
    float timeval2;
    float timeval;
    GameObject bullet;
    Rigidbody rigi;
    Rigidbody boss;
    public int health = 500;
    Vector3 leftup = new Vector3(-12, 0, 7);
    Vector3 leftbot = new Vector3(-12, 0, -7);
    Vector3 rightup = new Vector3(12, 0, 7);
    Vector3 rightbot = new Vector3(12, 0, -7);
    float distance = new Vector3(12, 0, 7).magnitude;
    public float qianyao = 0.7f;
    NavMeshAgent nav;
    Vector3 origin;
    GameObject door;
    int sanshetimes = 0;

    LineRenderer gunLine;
    Ray shootRay;
    RaycastHit shootHit;
    GameObject darkLight;
    GameObject Lightt;
    // Use this for initialization
    void Start()
    {
        bullet = (GameObject)Resources.Load("Prefabs/bounceBullet");
        player = GameObject.FindWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        boss = this.GetComponent<Rigidbody>();
        door = (GameObject)Resources.Load("Prefabs/Door");
        origin = this.transform.position;
        darkLight = (GameObject)Resources.Load("Prefabs/DarkLight");
        Lightt = (GameObject)Resources.Load("Prefabs/Light");
    }
    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
            Die();
        attackval += Time.deltaTime;
        timeval += Time.deltaTime;
        // this.transform.Rotate(new Vector3(0, 1, 0), rotaSpeed * Time.deltaTime);
        switch (state)
        {
            case 0:
                if (timeval <= qianyao)
                    qianyao1();
                else if (timeval <= statetime / 2)
                    nav.SetDestination(player.position);
                else
                {
                    Attack1();
                    timeval = 0;
                    sanshetimes++;
                }
                if (sanshetimes >= 2)
                {
                    state = 1;
                    break;
                }
                break;
            case 1://旋转X 

                // var child = this.GetComponentsInChildren<LineRen>();
                if (timeval <= 2.0f)
                {
                    Vector3 zero = transform.parent.position;

                    //transform.Translate((zero - transform.position).normalized * Time.deltaTime * 0.5f,Space.World);
                    nav.SetDestination(zero);
                }
                else
                {
                    this.transform.Rotate(new Vector3(0, 1, 0), rotaSpeed * Time.deltaTime);
                    foreach (Transform child in this.transform)
                    {
                        child.GetComponent<LineRenderer>().enabled = true;
                    }
                    if (timeval >= statetime)
                    {
                        state = 0;
                        timeval = 0;
                        timeval2 = 0;
                        attackval = 0;
                        sanshetimes = 0;
                        foreach (Transform child in this.transform)
                        {
                            child.GetComponent<LineRenderer>().enabled = false;
                        }
                    }
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
                rigi = Instantiate(bullet, this.transform.position + direction.normalized * 0.1f + new Vector3(0, 0.5f, 0), Quaternion.identity).GetComponent<Rigidbody>();
                //rigi.AddForce(direction.normalized * bulletSpeed);
                //rigi.velocity = direction.normalized * bulletSpeed;
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
        if(Physics.Raycast(shootRay,out shootHit, 20))
        {
            PlayerMovement player = shootHit.collider.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.DecreaseHealth();
                gunLine.SetPosition(1, shootHit.point);
            }
            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * 20);
            }
        }
    }





    void Die()
    {
        Instantiate(door, origin+new Vector3(3,0,3), Quaternion.identity);
        Instantiate(darkLight, this.transform.position + new Vector3(0, -1.0f, 0), Quaternion.identity);
        Instantiate(Lightt, this.transform.position + new Vector3(0.5f, -1.0f,0.5f), Quaternion.identity);
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

    void qianyao1()
    {

    }

    void qianyao2()
    {

    }
}
