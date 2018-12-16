using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerMove>().setJump();
        Destroy(this.gameObject);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
