using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward.normalized * 1000);
    }
	
	// Update is called once per frame
	void Update () {
    
    }
}
