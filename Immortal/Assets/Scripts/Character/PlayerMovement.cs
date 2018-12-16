using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    float speed;
    float timeval=0;
    public float shotcd = 0.2f;
    float normalspeed = 6.0f;
    float highspeed=9.0f;
    float camRayLength = 100f;
    float bulletForce = 1000;
    int floorMask;
    int ladderMask;
    int UIMask;
    Transform trans;
    public int light = 0;

    public int Health=200;

    public Transform outpoint;
    
    Rigidbody bulletrb;
    Rigidbody rb;
    GameObject bullet;
    Vector3 incr;
    Vector3 movement;
    Vector3 rayOrigin;


    // Use this for initialization
    void Start () {
        floorMask = LayerMask.GetMask("Floor");
        ladderMask = LayerMask.GetMask("Ladder");
        UIMask = LayerMask.GetMask("KnapsackCanvasSystem");
        rb = GetComponent<Rigidbody>();
        bullet = (GameObject)Resources.Load("Prefabs/PlayerBullet");
        trans = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Health <= 0)
            Die();
        timeval += Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            if (timeval >= shotcd)
            {
                if(Turning())
                Attack(bullet);
                timeval = 0;
            }
        }
        ChangeSpeed();
	}

    private void FixedUpdate()
    {
        //rb.velocity =new Vector3(0,0,0);
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rayOrigin = new Vector3(trans.position.x, trans.position.y, trans.position.z) + new Vector3(h, 0, v).normalized +trans.up*2;
        RaycastHit hit;
        
        if(Physics.Raycast(rayOrigin, new Vector3(0, -1, 0), out hit,100, ladderMask))
        {
            Debug.DrawLine(rayOrigin,hit.point, Color.red);
            
        }

        //trans.Translate(new Vector3(h, 0, v).normalized * speed * Time.deltaTime,Space.World);
        float hie=0;
      
           hie = 1.05f + hit.point.y - 0.1f;
        trans.Translate(h * speed * Time.deltaTime,0, v * speed * Time.deltaTime,Space.World);
        trans.Translate(0, hie - rb.position.y, 0,Space.World);
        //rb.MovePosition(new Vector3(rb.position.x,hie,rb.position.z));
        Turning();
    }

    void Move(float h,float v,float height)
    {
       // trans.Translate(new Vector3(h, 0, v).normalized * speed * Time.deltaTime);

       movement.Set(h, 0f, v);
       movement = movement.normalized * speed * Time.deltaTime;
       //movement.Set(movement.x, height, movement.z);
       rb.MovePosition(transform.position + movement);
    }  //

    bool Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (!Physics.Raycast(camRay, camRayLength, UIMask))
        { 
            if (Physics.Raycast(camRay, out floorHit, camRayLength))//这里的camray代表距离和方向
            {
                Vector3 playerToMouse = floorHit.point - this.transform.position;
                playerToMouse.y = 0;
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                rb.MoveRotation(newRotation);
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    void Attack(GameObject bullet)
    {
        Rigidbody bulltrans= Instantiate(bullet, this.transform.position, outpoint.rotation).GetComponent<Rigidbody>();
        bulltrans.AddForce(outpoint.forward.normalized * bulletForce);
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

    public void AddLight(int amount)
    {
        light += amount;
    }

   public void DecreaseHealth(int n=30)
    {
        Health -= n;
    }


    void Die()
    {
        Destroy(this.gameObject);
    }
} 
