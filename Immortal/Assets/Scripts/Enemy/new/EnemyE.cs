using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyE : MonoBehaviour
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

    int flag = 0;
    void Attack()
    {
        if (flag == 0)
        {
            Vector3 dire = new Vector3 (0, 0, 1);
            for (int i = 0; i < 4; i++)
            {
                bulletrigi = Instantiate(bullet, this.transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity).GetComponent<Rigidbody>();
                Vector3 dire1 = Quaternion.Euler(0, 90 * i, 0) * dire;
                bulletrigi.AddForce(dire1.normalized * bulletSpeed);
            }
            flag = 1;
        }
        else
        {
            Vector3 dire = new Vector3(0, 0, 1);
            dire = Quaternion.Euler(0, 45, 0) * dire;
            for (int i = 0; i < 4; i++)
            {
                bulletrigi = Instantiate(bullet, this.transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity).GetComponent<Rigidbody>();
                Vector3 dire1 = Quaternion.Euler(0, 90 * i, 0) * dire;
                bulletrigi.AddForce(dire1.normalized * bulletSpeed);
            }
            flag = 0;
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
