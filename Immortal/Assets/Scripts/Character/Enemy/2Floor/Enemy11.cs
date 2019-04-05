using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy11 : MonoBehaviour {

    float timeval = 1;
    Vector3 dire;
    Rigidbody bulletrigi;
    GameObject bullet;
    NavMeshAgent agent;
    Transform player;
    AudioSource audioSource;
    AudioClip EnemyShotEffect;
    AudioClip EnemyDie;

    public int Health=100;
    public float Attackcd = 0.3f;
    public float bulletSpeed = 1000;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<Transform>();
        dire = this.transform.forward;
        agent = this.GetComponent<NavMeshAgent>();
        bullet = (GameObject)Resources.Load("Prefabs/EnemyBullet");
        audioSource = this.GetComponent<AudioSource>();
        EnemyShotEffect = (AudioClip)Resources.Load("Music/EnemyBullet");
        EnemyDie = (AudioClip)Resources.Load("Music/EnemyDie");
    }
	
	// Update is called once per frame
	void Update () {
        if (Health <= 0)
            Die();
        timeval += Time.deltaTime;
        agent.SetDestination(player.position);
        if (timeval >= Attackcd)
        {
            Attack();
        }
	}

    void Attack()
    {
        bulletrigi = Instantiate(bullet, this.transform.position + dire.normalized * (1), Quaternion.identity).GetComponent<Rigidbody>();
        bulletrigi.AddForce(dire.normalized * bulletSpeed);
        dire = Quaternion.Euler(0, 30, 0) * dire;
        timeval = 0;
        audioSource.PlayOneShot(EnemyShotEffect);
    }

    private void Move()
    {
        
    }

    public void DecreaseHealth()
    {
        Health -= 20;
    }

    void Die()
    {
        audioSource.PlayOneShot(EnemyDie);
        Destroy(this.gameObject,0.5f);
    }
}
