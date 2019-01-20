using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : MonoBehaviour {

    public int health = 3;
    Rigidbody rigi;
    float timeval = 1;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            other.GetComponent<PlayerMovement>().DecreaseHealth(30);
            Destroy(this.gameObject);
        }
        if (other.name == "Wall1" || other.name == "Wall2")
        {
            rigi.velocity = new Vector3(rigi.velocity.x, rigi.velocity.y, -rigi.velocity.z);
            if (timeval >= 0.1f)
                health--;
        }
        else if (other.name == "Wall3" || other.name == "Wall4")
        {
            rigi.velocity = new Vector3(-rigi.velocity.x, rigi.velocity.y, rigi.velocity.z);
            if (timeval >= 0.1f)
                health--;
        }
       
    }

    // Use this for initialization
    void Start () {
        rigi = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
            Destroy(this.gameObject);
        timeval += Time.deltaTime;
	}
}
