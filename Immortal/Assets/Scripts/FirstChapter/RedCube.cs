using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCube : MonoBehaviour {

    public Transform[] corners;
    public float speed = 100f;
    public int id=0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate((corners[id].position - transform.position).normalized * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, corners[id].position) < 1f)
        {
            id = (id + 1) % 4;
        }
    }
}
