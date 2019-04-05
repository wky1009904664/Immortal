using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTree : MonoBehaviour {

    Transform player;
    Rigidbody bulletrigi;
    GameObject bullet;
    public GameObject bounceBall;

    public Transform claw;
    private  float timer = 0f;
    private float shootT = 0f;
    private float bounceT = 0f;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<Transform>();  //在当前场景
        //bounceBall = GameObject.Find("bounceBall");
        bullet = (GameObject)Resources.Load("Prefabs/EnemyBullet");
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        shootT += Time.deltaTime;
        bounceT += Time.deltaTime;
        this.transform.Rotate(new Vector3(0, 1, 0));

        if (timer > 4)
        {
            this.GetComponent<Animator>().SetTrigger("branch");
            timer = 0f;
        }
        if (shootT > 3)
        {
            Attack();
            shootT = 0f;
        }
        if (bounceT > 9)
        {
            Bounce();
            bounceT = 0f;
        }
	}

    void Bounce()
    {
        print("bounce");
        Vector3 direction = player.position - this.transform.position;
        if (bounceBall.activeSelf) bounceBall.SetActive(false);
        else {
            bounceBall.SetActive(true);
            bounceBall.transform.position = this.transform.position+new Vector3(0,0.3f,0);
            bounceBall.GetComponent<Rigidbody>().AddForce(direction.normalized * 2300);
        }
    }

    void Attack()
    {
        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;
        direction = Quaternion.Euler(0, -40, 0) * direction;
        for (int i = 0; i < 8; i++)
        {
            direction = Quaternion.Euler(0, 10, 0) * direction;
            for (int j = 0; j < 4; j++)
            {
                bulletrigi = Instantiate(bullet, 
                    this.transform.position + new Vector3(0, 0.4f, 0) + direction.normalized * (j), Quaternion.identity).GetComponent<Rigidbody>();
                bulletrigi.AddForce(direction.normalized * 1200);
            }
        }
        //audioSource.PlayOneShot(EnemyShotEffect);
       // timeval = 0;
    }
}
