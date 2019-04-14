using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyG : MonoBehaviour
{
    Transform player;
    GameObject bullet;
    AudioSource audioSource;
    AudioClip EnemyShotEffect;
    AudioClip EnemyDie;
    int Health = 2;
    NavMeshAgent agent;
    Rigidbody bulletrigi;
    public float bulletSpeed = 1000;
    public float shotcd = 0.5f;
    float timeval = 0;
    Vector3 dire;
    // Start is called before the first frame update
    void Start()
    {
        dire = this.transform.forward;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        bullet = (GameObject)Resources.Load("Prefabs/EnemyBullet");
        audioSource = this.GetComponent<AudioSource>();
        EnemyShotEffect = (AudioClip)Resources.Load("Music/EnemyBullet");
        EnemyDie = (AudioClip)Resources.Load("Music/EnemyDie");
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        timeval += Time.deltaTime;
        agent.SetDestination(player.position);
        if (timeval >= shotcd)
            Attack();
    }

    void Attack()
    {
        bulletrigi = Instantiate(bullet, this.transform.position + dire.normalized * (1), Quaternion.identity).GetComponent<Rigidbody>();
        bulletrigi.AddForce(dire.normalized * bulletSpeed);
        dire = Quaternion.Euler(0, 30, 0) * dire;
        timeval = 0;
        audioSource.PlayOneShot(EnemyShotEffect);
    }

    public void DecreaseHealth()
    {
        Health--;
        if (Health <= 0)
            Die();
    }


    void Die()
    {
        audioSource.PlayOneShot(EnemyDie);
        Destroy(this.gameObject);
    }

}
