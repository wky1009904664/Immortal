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


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        bullet = (GameObject)Resources.Load("Prefabs/EnemyBullet");
        audioSource = this.GetComponent<AudioSource>();
        EnemyShotEffect = (AudioClip)Resources.Load("Music/EnemyBullet");
        EnemyDie = (AudioClip)Resources.Load("Music/EnemyDie");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
