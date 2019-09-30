using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : MonoBehaviour {

    public int health = 3;
    Rigidbody rigi;
    float timeval = 1;

    // Use this for initialization
    void Start () {
        rigi = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<PlayerMovement>().DecreaseHealth();
            Destroy(this.gameObject);
        }
        else  if (collision.transform.tag != "Enemy" && collision.transform.tag != "Bullet")
             {
            health--;
            if (health == 0)
                Destroy(this.gameObject);
        }
       // else
       
    }
}
