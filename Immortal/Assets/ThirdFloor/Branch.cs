using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour {

    public Transform claw1;
    public Transform claw2;

	// Use this for initialization
	void Start () {
	
	}

    private void OnTriggerStay(Collider other)
    {
       // print(Vector3.Distance(other.transform.position, this.transform.position));
        if (other.gameObject.tag == "Player" && Vector3.Distance(other.transform.position, this.transform.position) > 6)
        {
            if (Vector3.Distance(other.transform.position, claw1.position) <
                Vector3.Distance(other.transform.position, claw2.position))
                other.gameObject.transform.position = claw1.position;
            else other.gameObject.transform.position = claw2.position;
        }
    }



    // Update is called once per frame
    void Update () {
  
	}
}
