using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gear : MonoBehaviour {

    GameObject redCube;
    GameObject yellowCube;
    GameObject blueCube;
    private Color color;
    private float ralpha = -1f;
    private float yalpha = -1f;
    private float balpha = -1f;

    private bool red=false;
    private bool yellow=false;
    private bool blue = false;
    private Text text;
    private GameObject redK;
    private GameObject yellowK;
    private GameObject blueK;
    public GameObject panel;

    // Use this for initialization
    void Start () {
        text = panel.GetComponentInChildren<Text>();
        color = panel.GetComponent<Image>().color;
        panel.SetActive(false);
        redCube = GameObject.Find("redCube");
        yellowCube = GameObject.Find("yellowCube");
        blueCube = GameObject.Find("blueCube");
  
        redK = GameObject.Find("redK");
        yellowK = GameObject.Find("yellowK");
        blueK = GameObject.Find("blueK");
    }
	
	void Update () {

        Vector3 pos = redK.transform.InverseTransformPoint(redCube.transform.position);

        if (!red && pos.x < 1 && pos.x > 0.5 && pos.z > -0.3 && pos.z < 0.3) { red = true; panel.SetActive(true); text.text ="Red is finished."; }
        if (red)
        {
            ralpha += Time.deltaTime;
            if (ralpha > 0.95) panel.SetActive(false);
            else panel.GetComponent<Image>().color = new Color(color.r, color.g, color.b, Mathf.Lerp(0.7f, 0, ralpha));
        }

        pos = yellowK.transform.InverseTransformPoint(yellowCube.transform.position);
        if (pos.x < 1 && pos.x > 0.5 && pos.z > -0.3 && pos.z < 0.3) { yellow = true; panel.SetActive(true); text.text = "Yellow is finished."; }
        if (yellow)
        {
            yalpha += Time.deltaTime;
            if (yalpha > 0.95) panel.SetActive(false);
            else panel.GetComponent<Image>().color = new Color(color.r, color.g, color.b, Mathf.Lerp(0.7f, 0, yalpha));
        }

        pos = blueK.transform.InverseTransformPoint(blueCube.transform.position);
        if (pos.x < 1 && pos.x > 0.5 && pos.z > -0.3 && pos.z < 0.3) { blue = true; panel.SetActive(true); text.text = "Blue is finished."; }
        if(blue)
        {
            balpha += Time.deltaTime;
            if (balpha > 0.95) panel.SetActive(false); 
            else panel.GetComponent<Image>().color = new Color(color.r, color.g, color.b, Mathf.Lerp(0.7f, 0, balpha));
        }

        if (!red && !yellow && !blue)
        {
            Camera.main.GetComponent<CameraMove>().Move1();
            Destroy(this);
        }
    }
}
