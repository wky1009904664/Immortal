using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseProperty//共有属性类
{
    public int ID { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string sprite { get; set; }
    public int canbeused { get; set; }

    public BaseProperty(int ID, string name, string description, int canbeused)
    {
        this.ID = ID;
        this.name = name;
        this.description = description;
        this.sprite = sprite;
        this.canbeused = canbeused;
    }

    public BaseProperty()
    {

    }

}

public class KnapsackGood {//基类

    public BaseProperty goodProperty { get; set; }
    public KnapsackGood(BaseProperty goodProperty)
    {
        this.goodProperty = goodProperty;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string GetDescribe()
    {
        string describe = goodProperty.description;
        return describe;
    }
}
