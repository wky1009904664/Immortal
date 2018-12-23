using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gear : MonoBehaviour {

    GameObject redCube;
    GameObject yellowCube;
    GameObject blueCube;
    private Color color;
    private float alpha = -1.5f;
    private bool red=false;
    private bool yellow=false;
    private bool blue = false;

    public GameObject panel;

    // Use this for initialization
    void Start () {
        color = panel.GetComponent<Image>().color;
        panel.SetActive(false);
        redCube = GameObject.Find("redCube");
        yellowCube = GameObject.Find("yellowCube");
        blueCube = GameObject.Find("blueCube");
    }
	
	// Update is called once per frame
	void Update () {
        if (redCube.transform.position.x < -19 && redCube.transform.position.z > 11.7 && redCube.transform.position.z < 12.3)
        {
            panel.SetActive(true);
            red = true;
        }
        if (panel.activeSelf)
        {
            alpha += Time.deltaTime;
            panel.GetComponent<Image>().color = new Color(color.r, color.g, color.b, Mathf.Lerp(0.8f, 0, alpha));
            if (alpha > 0.95)
                panel.SetActive(false);
        }
    }
}
