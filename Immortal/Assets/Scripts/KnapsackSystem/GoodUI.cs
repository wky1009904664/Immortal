using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoodUI : MonoBehaviour {

    public KnapsackGood good { get; private set; }
    public int amount { get; private set; }

    private Image GoodUIImage
    {
        get
        {
            return this.transform.GetComponent<Image>();
        }
    }

    public void SetGood(KnapsackGood good)
    {
        this.good = good;
        GoodUIImage.sprite = Resources.Load<Sprite>(good.goodProperty.sprite);
        
    }



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
