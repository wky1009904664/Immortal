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
    public int Health=1000;
    Transform player;
    GameObject bullet;
    GameObject darkLight;
    Rigidbody bulletrigi;
    float timeval = 10;
    int count = 0;
    AudioSource audioSource;
    AudioClip EnemyShotEffect;
    AudioClip EnemyDie;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        bullet = (GameObject)Resources.Load("Prefabs/EnemyBullet");
        darkLight = (GameObject)Resources.Load("Prefabs/Light");
    }

    // Update is called once per frame
    void Update()
    {
        Alert();
       
        if (Health <= 0)
            Die();
        timeval += Time.deltaTime;
        if (timeval >= Attackcd)
        {
           // if (count % 5 != 0)
           // {
                if (AlertIsTrue)
                {
                    Attack();

                }
          //  }
        }
    }

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
            direction = Quaternion.Euler(0, 360 / shotAmount, 0) * direction;
            if (i % 5 == 0||i%4==0)
                continue;
            for (int j = 0; j < 4; j++)
            {
                bulletrigi = Instantiate(bullet, this.transform.position + new Vector3(0, 0.4f, 0) + direction.normalized * (j), Quaternion.identity).GetComponent<Rigidbody>();
                bulletrigi.AddForce(direction.normalized * bulletSpeed);
            }
        }
        audioSource.PlayOneShot(EnemyShotEffect);
        timeval = 0;
    }

    public void DecreaseHealth( )
    {
        Health -= 20;
    }


    void Die()
    {
        audioSource.PlayOneShot(EnemyDie);
        float dis = player.position.y - this.transform.position.y;
        Instantiate(darkLight,this.transform.position + new Vector3(0, dis, 0), Quaternion.identity);
        Destroy(this.gameObject);
    }

}
