using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyF : MonoBehaviour
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
        for (int i = -1; i < 2; i++)
        {
            bulletrigi = Instantiate(bullet, this.transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity).GetComponent<Rigidbody>();
            Debug.Log(i);
            Vector3 dire1 = Quaternion.Euler(0, 30 * i, 0) * direction;

            direction.y = 0;
            bulletrigi.AddForce(dire1.normalized * bulletSpeed);
        }
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
