using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyH : MonoBehaviour
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
    public int shotAmount=10;
    // Start is called before the first frame update
    void Start()
    {
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
        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;
        for (int i = 0; i < shotAmount; i++)
        {
            direction = Quaternion.Euler(0, 360 / shotAmount, 0) * direction;     
                bulletrigi = Instantiate(bullet, this.transform.position + direction.normalized , Quaternion.identity).GetComponent<Rigidbody>();
                bulletrigi.AddForce(direction.normalized * bulletSpeed);
        }
        audioSource.PlayOneShot(EnemyShotEffect);
        timeval = 0;
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
