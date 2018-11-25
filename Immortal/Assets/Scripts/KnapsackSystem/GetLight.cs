using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetLight : MonoBehaviour {

    PlayerMovement player;
    public int amount = 10;
    public Text text;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.AddLight(amount);
            text.text = string.Format("光团：{0}", player.light);
            Destroy(gameObject);
        }
    }
}
