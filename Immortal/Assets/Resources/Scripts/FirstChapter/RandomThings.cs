using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomThings : MonoBehaviour {

    public Transform[] pos;
    public GameObject zhu;
    private int a, b;

	// Use this for initialization
	void Start () {
        a = Random.Range(0, pos.Length);
        b = Random.Range(0, pos.Length);
        while (a == b) b = Random.Range(0, pos.Length);
        GameObject.Instantiate(zhu, pos[a].position,Quaternion.identity);
        GameObject.Instantiate(zhu, pos[b].position,Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
