using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : MonoBehaviour
{
    Transform player;
    GameObject bullet;
    AudioSource audioSource;
    AudioClip EnemyShotEffect;
    AudioClip EnemyDie;
    float speed = 2;
    GameObject Explode;
    public float explodeRange = 1;
    public float demageRange = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        bullet = (GameObject)Resources.Load("Prefabs/EnemyBullet");
        audioSource = this.GetComponent<AudioSource>();
        EnemyShotEffect = (AudioClip)Resources.Load("Music/EnemyBullet");
        EnemyDie = (AudioClip)Resources.Load("Music/EnemyDie");
        Explode = (GameObject)Resources.Load("ConfettiBlastBlue");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dis = player.position - transform.position;
        dis.y = 0;
        transform.Translate(dis.normalized * speed * Time.deltaTime);
        if (dis.magnitude <= explodeRange)
        {
            Instantiate(Explode);
          
            if (dis.magnitude <= demageRange)
                player.GetComponent<PlayerMovement>().DecreaseHealth();
            Destroy(this.gameObject);
        }
    }

    
}
