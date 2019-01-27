using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class F2Start : MonoBehaviour {

    Rigidbody rigi;
    bool isjump = false;
    int state = 0;//0是在礁石上，1是在云上

    public float jumpForce = 250;
    public float speed =10;
    Transform moon1;
    Transform moon2;
    bool ismoon1 = false;
    bool ismoon2 = false;

	// Use this for initialization
	void Start () {
        rigi = GetComponent<Rigidbody>();
        moon1 = GameObject.Find("Moon1").GetComponent<Transform>();
        moon2 = GameObject.Find("Moon2").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (ismoon1 && ismoon2)
        {
            SceneManager.LoadScene(4);
        }
	}

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isjump)
            {
                if (state == 0)
                    rigi.AddForce(transform.up * jumpForce);
                else
                    rigi.AddForce(-transform.up * jumpForce);
                isjump = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Vector3.Distance(transform.position, moon1.position)<=3)
            {
                moon1.GetComponent<MoonBack>().ShareMaterial();
                ismoon1 = true;
            }
            if (Vector3.Distance(transform.position, moon2.position)<=3)
            {
                moon2.GetComponent<MoonBack>().ShareMaterial();
                ismoon2 = true;
            }
        }
    }
       
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Barrier")
            isjump = false;
        if (collision.collider.tag == "Floor")
        {
            state = state == 0 ? 1 : 0;
            if (state == 0)
            {
                Physics.gravity = new Vector3(0, -10, 0);
                transform.position = new Vector3(6.19f, 2.6f, -13.44f);
            }
            else
            {
                Physics.gravity = new Vector3(0, 10, 0);
                transform.position = new Vector3(7.98f, 17 , -14);
            }
        }
    }
}
