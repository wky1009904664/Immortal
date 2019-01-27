using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBack : MonoBehaviour {

    Material meshRender;
    public Renderer rend;
    public Texture textrue;

	// Use this for initialization
	void Start () {
        meshRender = Resources.Load("Moon") as Material;
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShareMaterial()
    {
        rend.sharedMaterial = meshRender;
    }
}