using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    float speed;
    float timeval=1;
    float timeval2 =1;
    float timeval3 = 1;
    public float shotcd = 0.1f;
    public float clearcd = 5.0f;
    float normalspeed = 9.0f;
    float highspeed=13.0f;
    float camRayLength = 100f;
    float bulletForce = 1000;
    int floorMask;
    int ladderMask;
    int UIMask;
    Transform trans;
    public int lightt = 0;
    public int darkLight = 0;
    public float JumpHeight=1;
    public int Health=200;
    public float JumpForce;
    public GameObject DeathImage;
    public Transform outpoint;
    
    Rigidbody bulletrb;
    Rigidbody rb;
    GameObject bullet;
    Vector3 incr;
    Vector3 movement;
    Vector3 rayOrigin;
    AudioSource audioSource;
    AudioClip PlayerShot;
    AudioClip PlayerHurt;

    public GameObject myBall;
    public Gear31 gear31;
    private int Joker;

    // Use this for initialization
    void Start () {
        Joker = 0;
        floorMask = LayerMask.GetMask("Floor");
        ladderMask = LayerMask.GetMask("Ladder");
        UIMask = LayerMask.GetMask("KnapsackCanvasSystem");
        rb = GetComponent<Rigidbody>();
        bullet = (GameObject)Resources.Load("Prefabs/PlayerBullet");
        audioSource = this.GetComponent<AudioSource>();
        PlayerShot = (AudioClip)Resources.Load("Music/PlayerBullet");
        PlayerHurt = (AudioClip)Resources.Load("Music/BulletHurtLine");
        trans = GetComponent<Transform>();
    }
	
    public void ChangeBall(int Joker)
    {
        this.Joker += Joker;
        switch (this.Joker)
        {
            case 1:
            case 2:
                myBall.GetComponent<Renderer>().material.color = Color.red;
                break;

            case 10:
            case 20:
                myBall.GetComponent<Renderer>().material.color = Color.green;
                break;

            case 100:
            case 200:
                myBall.GetComponent<Renderer>().material.color = Color.blue;
                break;

            case 11:
            case 12:
            case 21:
            case 22:
                myBall.GetComponent<Renderer>().material.color = Color.yellow;
                break;

            case 110:
            case 120:
            case 210:
            case 220:
                myBall.GetComponent<Renderer>().material.color = Color.cyan;
                break;

            case 101:
            case 102:
            case 201:
            case 202:
                myBall.GetComponent<Renderer>().material.color = Color.magenta;
                break;
            default:
                myBall.GetComponent<Renderer>().material.color = Color.white;
                break;
        }
        myBall.SetActive(true);
    }

    private void putBall()
    {
        switch (Joker)
        {
            case 1:
            case 2:
                Instantiate(Resources.Load("Prefabs/ColorBalls/red"), this.transform.position, Quaternion.identity);
                gear31.triggerGear("red", this.transform);
                break;

            case 10:
            case 20:
                Instantiate(Resources.Load("Prefabs/ColorBalls/green"), this.transform.position, Quaternion.identity);
                break;

            case 100:
            case 200:
                Instantiate(Resources.Load("Prefabs/ColorBalls/blue"), this.transform.position, Quaternion.identity);
                break;

            case 11:
            case 12:
            case 21:
            case 22:
                Instantiate(Resources.Load("Prefabs/ColorBalls/yellow"), this.transform.position, Quaternion.identity);
                gear31.triggerGear("yellow", this.transform);
                break;

            case 110:
            case 120:
            case 210:
            case 220:
                Instantiate(Resources.Load("Prefabs/ColorBalls/cyan"), this.transform.position, Quaternion.identity);
                gear31.triggerGear("cyan", this.transform);
                break;

            case 101:
            case 102:
            case 201:
            case 202:
                Instantiate(Resources.Load("Prefabs/ColorBalls/magenta"), this.transform.position, Quaternion.identity);
                break;
            default:
                Instantiate(Resources.Load("Prefabs/ColorBalls/white"), this.transform.position, Quaternion.identity);
                break;
        }
        myBall.SetActive(false);
        Joker = 0;
    }

	// Update is called once per frame
	void Update () {
        if (Health <= 0)
            Die();
        timeval += Time.deltaTime;
        timeval2 += Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            if (timeval >= shotcd)
            {
               if(Turning())
               Attack(bullet);
                timeval = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (timeval2 >= clearcd)
            {
                ClearScreen();
                timeval2 = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            putBall();
           
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
      //  trans.Translate(0, hie - rb.position.y, 0,Space.World);
        //rb.MovePosition(new Vector3(rb.position.x,hie,rb.position.z));
        Turning();
        
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
        audioSource.PlayOneShot(PlayerShot);
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
        lightt += amount;
    }

    public void AddDarkLight(int amount)
    {
        darkLight += amount;
    }

   public void DecreaseHealth(int n=30)
    {
        Health -= n;
        audioSource.PlayOneShot(PlayerHurt);
    }


    void Die()
    {
        DeathImage.SetActive(true);
        Destroy(this.gameObject);
    }

    void ClearScreen()
    {
        var bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach(var bullet in bullets)
        {
            Destroy(bullet);
        }
    }
} 
