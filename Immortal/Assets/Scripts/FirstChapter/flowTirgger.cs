using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowTirgger : MonoBehaviour {

    public FirstPerson eyes;

	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name=="Player")
        {
            eyes.setFlow();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
