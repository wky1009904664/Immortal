using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    private float timer;
    private  float distance=4;

    private void OnTriggerEnter(Collider other)
    {
        // print(other.tag);
        Debug.Log(1);
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().DecreaseHealth(30);
        }
            //other.SendMessage("DecreaseHealth");
        
        if (other.tag != "Enemy" && other.tag != "Bullet")
            Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        timer = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > distance) Destroy(this.gameObject);
    }
}
