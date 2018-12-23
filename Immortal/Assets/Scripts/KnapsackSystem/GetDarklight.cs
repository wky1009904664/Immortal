using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetDarklight : MonoBehaviour {

    PlayerMovement player;
    public int amount = 10;
    Text text;
    GameObject go;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        text = GameObject.Find("KnapsackCanvasSystem/KnapsackCanvas/KnapsackPanel/DarkLight/Text").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.AddDarkLight(amount);
            text.text = string.Format("暗团：{0}", player.darkLight);
            Destroy(gameObject);
        }
    }
}
