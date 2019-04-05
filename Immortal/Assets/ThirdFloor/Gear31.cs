using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear31 : MonoBehaviour {

    public Transform rt;
    public Transform yt;
    public Transform ct;

    public GameObject pool1;
    public GameObject pool2;
    public GameObject pool3;

    // Use this for initialization
    void Start () {
		
	}
	
    public void triggerGear(string name,Transform ball)
    {
        switch (name)
        {
            case "yellow":
                if (Vector3.Distance(yt.position, ball.position) < 5)
                {
                    pool1.GetComponent<Animation>().Play("pool1up");
                    print("why");
                }
                break;
            case "red":
                if (Vector3.Distance(rt.position, ball.position) < 5)
                    pool2.GetComponent<Animation>().Play("pool2up");
                break;
            case "cyan":
                if (Vector3.Distance(ct.position, ball.position) < 5)
                    pool3.GetComponent<Animation>().Play("pool3up");
                break;
        }
    }

  
}
