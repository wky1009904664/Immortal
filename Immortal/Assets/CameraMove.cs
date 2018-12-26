using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public float speed=0f;
    private bool move1 = false;
    private bool dec = false;

	// Use this for initialization
	void Start () {
		
	}

    public void Move1()
    {
        move1 = true;
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);

        if (speed < 0) move1 = false;
        if (speed < 40 && !dec) speed += 0.5f;
        else { dec = true; speed -= 0.5f; }
    }
	
	// Update is called once per frame
	void Update () {
        if(move1) Move1();
	}
}
