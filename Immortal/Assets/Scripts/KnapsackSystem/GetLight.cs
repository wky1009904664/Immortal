﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetLight : MonoBehaviour {

    PlayerMovement player;
    public int amount = 1;
    Text text;
    GameObject go;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        text = GameObject.Find("KnapsackCanvasSystem/KnapsackCanvas/KnapsackPanel/Light/Text").GetComponent<Text>();
        
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.AddLight(amount);
            text.text = string.Format("光团：{0}", player.lightt);
            Destroy(gameObject);
        }
    }
}
