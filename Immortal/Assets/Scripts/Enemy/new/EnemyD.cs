using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyD : MonoBehaviour
{
    Transform player;
    GameObject bullet;
    AudioSource audioSource;
    AudioClip EnemyShotEffect;
    AudioClip EnemyDie;
    int Health = 3;

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
    }

    // Update is called once per frame
    void Update()
    {
        timeval += Time.deltaTime;
        if (timeval >= shotcd)
            Attack();
    }

    void Attack()
    {
        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;
        bulletrigi = Instantiate(bullet, this.transform.position  + direction.normalized , Quaternion.identity).GetComponent<Rigidbody>();
        bulletrigi.AddForce(direction.normalized * bulletSpeed);
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
