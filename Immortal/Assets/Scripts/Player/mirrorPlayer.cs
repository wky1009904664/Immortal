using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorPlayer: MonoBehaviour
{

    float speed;
    float timeval = 1;
    float timeval3 = 1;
    float normalspeed = 9.0f;
    float highspeed = 13.0f;
    float camRayLength = 100f;
    float bulletForce = 1000;
    int floorMask;
    int ladderMask;
    int UIMask;
    Transform trans;
    public int lightt = 0;
    public int darkLight = 0;
    public float JumpHeight = 1;
    public int Health = 200;

    public PlayerMovement pmt;

    Rigidbody bulletrb;
    Rigidbody rb;
    GameObject bullet;
    Vector3 incr;
    Vector3 movement;
    Vector3 rayOrigin;


    // Use this for initialization
    void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        ladderMask = LayerMask.GetMask("Ladder");
        UIMask = LayerMask.GetMask("KnapsackCanvasSystem");
        rb = GetComponent<Rigidbody>();
        bullet = (GameObject)Resources.Load("Prefabs/PlayerBullet");
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)   Die();
        timeval += Time.deltaTime;
        timeval3 += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("1");
            if (timeval3 >= 1.0f)
            {
                this.GetComponent<Rigidbody>().AddForce(this.transform.up * JumpHeight);
                timeval3 = 0;
            }
        }
        ChangeSpeed();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "red"|| collision.gameObject.name == "red(Clone)")
        {
            Destroy(collision.gameObject);
            pmt.ChangeBall(1);
        }
        if (collision.gameObject.name == "green" || collision.gameObject.name == "green(Clone)")
        {
            Destroy(collision.gameObject);
            pmt.ChangeBall(10);
        }
        else if (collision.gameObject.name == "blue" || collision.gameObject.name == "blue(Clone)")
        {
            Destroy(collision.gameObject);
            pmt.ChangeBall(100);
        }
    }

    private void FixedUpdate()
    {
        //rb.velocity =new Vector3(0,0,0);
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rayOrigin = new Vector3(trans.position.x, trans.position.y, trans.position.z) + new Vector3(h, 0, v).normalized + trans.up * 2;
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, new Vector3(0, -1, 0), out hit, 100, ladderMask))
        {
            Debug.DrawLine(rayOrigin, hit.point, Color.red);

        }

        float hie = 0;

        hie = 1.05f + hit.point.y - 0.1f;
        trans.Translate(-h * speed * Time.deltaTime, 0, -v * speed * Time.deltaTime, Space.World);
        trans.Translate(0, hie - rb.position.y, 0, Space.World);
        //rb.MovePosition(new Vector3(rb.position.x,hie,rb.position.z));
        Turning();
    }

    void Move(float h, float v, float height)
    {

        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }  

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



    void ChangeSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            speed = highspeed;
        else
            speed = normalspeed;
    }
    public void DecreaseHealth(int n = 30)
    {
        Health -= n;
    }


    void Die()
    {
        Destroy(this.gameObject);
    }

}
