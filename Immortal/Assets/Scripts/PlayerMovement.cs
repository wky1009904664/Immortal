using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    float speed;
    float timeval;
    public float shotcd = 0.2f;
    float normalspeed = 6.0f;
    float highspeed=9.0f;
    float camRayLength = 100f;
    float bulletForce = 1000;
    int floorMask;
    Transform trans;
    public Transform outpoint;

    Rigidbody bulletrb;
    Rigidbody rb;
    GameObject bullet;
    Vector3 incr;
    Vector3 movement;



    // Use this for initialization
    void Start () {
        floorMask = LayerMask.GetMask("Floor");
        rb = GetComponent<Rigidbody>();
        bullet = (GameObject)Resources.Load("Prefabs/Bullet");
        trans = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        timeval += Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            if (timeval >= shotcd)
            {
                Attack(bullet);
                timeval = 0;
            }
        }
        ChangeSpeed();
	}

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Turning();
    }

    void Move(float h,float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))//这里的camray代表距离和方向
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
            
        }
    }

    void Attack(GameObject bullet)
    {
        Instantiate(bullet, outpoint.position, outpoint.rotation);
       //bulletrb = bullet.GetComponent<Rigidbody>();
       //bulletrb.AddForce(outpoint.forward.normalized*bulletForce);
       //Debug.Log(outpoint.forward.normalized * bulletForce);
    }
    
    void ChangeSpeed()
    {
        if(Input.GetKey(KeyCode.LeftShift))
            speed = highspeed;
        else
            speed = normalspeed;
    }

}
