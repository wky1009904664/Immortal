using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Gear31 : MonoBehaviour {

    public Transform rt;
    public Transform yt;
    public Transform ct;

    public GameObject pool1;
    public GameObject pool2;
    public GameObject pool3;

    bool bo1 = false;
    bool bo2 = false;
    bool bo3 = false;

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
                    bo1 = true;
                }
                break;
            case "red":
                if (Vector3.Distance(rt.position, ball.position) < 5)
                {
                    pool2.GetComponent<Animation>().Play("pool2up");
                    bo2 = true;
                }
                break;
            case "cyan":
                if (Vector3.Distance(ct.position, ball.position) < 5)
                {
                    pool3.GetComponent<Animation>().Play("pool3up");
                    bo3 = true;
                }
                break;
        }
    }

    private void LateUpdate()
    {
        if (bo1 && bo2 && bo3)
            SceneManager.LoadScene(8);
            
    }
}
