using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy22 : MonoBehaviour {

    float timeval = 0;
    Vector3 dire;
    Rigidbody bulletrigi;
    GameObject bullet;
    bool rise = true;
    bool shotted=false;
    Vector3 down;
    Vector3 up;
    
    //-1.33  0.9
    public float uptime=2.0f;//上浮时间
    public int Health = 100;
    public float Attackcd = 0.3f;
    public float bulletSpeed = 1000;
    public int shotAmount = 20;
    float speed;
    // Use this for initialization
    void Start () {
        dire = this.transform.forward;
        bullet = (GameObject)Resources.Load("Prefabs/EnemyBullet");
        down = this.transform.position;
        up = down + new Vector3(0, 2.23f, 0);
        speed = 1 / uptime;
    }
	
	// Update is called once per frame
	void Update () {
        if (Health <= 0)
            Die();
        timeval += Time.deltaTime;
        if (timeval <= uptime)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, up+new Vector3(0, 1, 0), Time.deltaTime*speed);
        }
        else if (timeval <= uptime + 1)
        {
            if (!shotted)
                Attack();
        }
        else if (timeval <= 2 * uptime + 1)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, down + new Vector3(0, -1.2f, 0), Time.deltaTime * speed);
        }
        else if(timeval>=2*uptime+2)
        {
            timeval = 0;
            shotted = false;
        }
    }

    void Attack()
    {
        Vector3 direction = this.transform.forward;
        direction.y = 0;
        for (int i = 0; i < shotAmount; i++)
        {
            direction = Quaternion.Euler(0, 360 / shotAmount, 0) * direction;
            bulletrigi = Instantiate(bullet, this.transform.position + new Vector3(0, 0.4f, 0) + direction.normalized , Quaternion.identity).GetComponent<Rigidbody>();
            bulletrigi.AddForce(direction.normalized * bulletSpeed);
        }
        timeval = 0;
        shotted = true;
    }

    public void DecreaseHealth()
    {
        Health -= 20;
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<PlayerMovement>().DecreaseHealth();
        }
    }
}
