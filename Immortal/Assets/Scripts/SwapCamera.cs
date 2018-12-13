using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCamera : MonoBehaviour {

    private GameObject mainCamera;
    private GameObject camera2;
    public SetMirrorCP mirrorPlane;
    public int k;
    public static SwapCamera Instance;

	// Use this for initialization
	void Start () {
        Instance = this;
        k = 0;
        mainCamera = GameObject.Find("Camera0");
        camera2 = GameObject.Find("Camera1");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            k = 1 - k;
            if (k > 0)
            {
                mainCamera.SetActive(false);
                camera2.SetActive(true);
            } else
            {
                mainCamera.SetActive(true);
                camera2.SetActive(false);
            }
        }
    }
}
