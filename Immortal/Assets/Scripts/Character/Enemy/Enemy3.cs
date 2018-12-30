using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour {

    public float rotaSpeed;
    public int shotAmount;
    Rigidbody bulletrigi;
    public float bulletSpeed;
    private GameObject bullet;
    float timeval = 0;
    Transform player;
    public float shotcd;
    public float walkSpeed;
    Vector3 origin;
    GameObject door;
    float time1;
    public int Health = 500;
    GameObject Lightt;
    // Use this for initialization
    void Start () {
        bullet = (GameObject)Resources.Load("Prefabs/EnemyBullet");
        player = GameObject.Find("Player").GetComponent<Transform>();
        door = (GameObject)Resources.Load("Prefabs/Door");
        origin = this.transform.position;
        Lightt = (GameObject)Resources.Load("Prefabs/Light");
    }
	
	// Update is called once per frame
	void Update () {
        if (Health <= 0)
            Die();
        timeval += Time.deltaTime;
        time1 += Time.deltaTime;
        this.transform.Rotate(new Vector3(0, 1, 0), rotaSpeed * Time.deltaTime);
        if (time1 > 2.0f)
        {
            if (timeval >= shotcd)
                Attack();
            Move();
        }
    }

    void Attack()
    {
        Vector3 direction = this.transform.forward;
        direction.y = 0;
        for (int i = 0; i < shotAmount; i++)
        {
            direction = Quaternion.Euler(0, 360 / shotAmount, 0) * direction;
           // for (int j = 0; j < 4; j++)
           // {
                bulletrigi = Instantiate(bullet, this.transform.position + new Vector3(0, 0.4f, 0) + direction.normalized, Quaternion.identity).GetComponent<Rigidbody>();
                bulletrigi.AddForce(direction.normalized * bulletSpeed);
          //  }
        }

        timeval = 0;
    }

    void Move()
    {
        Vector3 dis = player.position - this.transform.position;
        this.transform.Translate(dis.normalized * walkSpeed*Time.deltaTime,Space.World);
    }


    public void DecreaseHealth()
    {
        Health -= 20;
    }

    void Die()
    {
        float dis = player.position.y - this.transform.position.y;
        Instantiate(door, origin + new Vector3(3, 0, 3), Quaternion.identity);
        Instantiate(Lightt, this.transform.position + new Vector3(0, -1.0f, 0), Quaternion.identity);
        Destroy(this.gameObject);
    }
}
