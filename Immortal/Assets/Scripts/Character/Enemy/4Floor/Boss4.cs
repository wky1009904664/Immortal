using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4 : MonoBehaviour {

    int state = 0;
    int flag = 0;
    bool is4state = false;
    Vector3 direction;

    public int Health = 500;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    	
	}

    void ChangeState()
    {
        if (Health <= 500)
            state = 1;
        else if (Health <= 250 && !is4state)
            state = 2;
        else if (is4state)
            state = 3;
        
    }

    void Attack0()
    {

    }

    void Attack1()
    {
        if (flag == 0)
        {
            //前后实例化，Addforce
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                direction = Quaternion.Euler(0, 360 / 5, 0) * direction;
               
            }
        }
    }
}
