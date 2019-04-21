using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyC : MonoBehaviour
{
    Transform player;
    GameObject bullet;
    AudioSource audioSource;
    AudioClip EnemyShotEffect;
    AudioClip EnemyDie;
    int Health = 2;
    NavMeshAgent agent;

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
        agent.SetDestination(player.position);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<PlayerMovement>().DecreaseHealth();
        }
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
