using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Alert1 : MonoBehaviour {
    [Range(0, 10)]
    public float AlertRadius;

    [Range(0, 360)]
    public float Alertangle;


    public bool AlertIsTrue = false;
    public Vector3 wanderDirection;
    public float wanderDistance;
    public float walkspeed;
    public float bulletSpeed;
    public float Attackcd;
    public float turnspeed;

    Rigidbody rigi;
    Transform Playertans;
    Transform player;
    Vector3 origin;
    GameObject bullet;
    Rigidbody bulletrigi;

    float timeval = 0;
    int flag = 0;

    // private void OnDrawGizmos()
    // {
    //     Color color = Handles.color;
    //     Handles.color = Color.gray;
    //     Vector3 StartLine = Quaternion.Euler(0, -Alertangle, 0) * this.transform.forward;
    //     Handles.DrawSolidArc(this.transform.position, this.transform.up, StartLine, Alertangle, AlertRadius);
    //     Handles.color = color;
    // }

    void Alert()
    {
        Vector3 dis = player.position - this.transform.position;
        float distance = dis.magnitude;
        float disAngle = Vector3.Angle(dis, this.transform.forward);
        if (distance <= AlertRadius && disAngle <= Alertangle)
        {
            AlertIsTrue = true;
        }
        else
        {
            AlertIsTrue = false;
        }
    }

    void Wander()
    {

        Vector3 target1 = origin + wanderDirection * wanderDistance;
        Vector3 target2 = origin - wanderDirection * wanderDistance;
        //Quaternion changedire = Quaternion.Euler(0, 180, 0);
       // Debug.Log(Vector3.Distance(this.transform.position, target1));
        if (flag == 0)//朝1走
        {
            //rigi.velocity = wanderDirection.normalized * walkspeed;
            this.transform.Translate((wanderDirection) * walkspeed * Time.deltaTime, Space.World);
            Quaternion quat = Quaternion.LookRotation(target1 - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(transform.rotation, quat, turnspeed);
            Debug.Log(quat.eulerAngles);
            if (Vector3.Distance(this.transform.position, target1) <=1f)
            {
                flag = 1;
            }
        }
        if (flag == 1)
        {
            this.transform.Translate(-wanderDirection * walkspeed * Time.deltaTime, Space.World);
            Quaternion quat = Quaternion.LookRotation(target2-this.transform.position);
            Quaternion rota = Quaternion.Euler(0, 90, 0);
            Debug.Log(quat.eulerAngles);
            this.transform.rotation = Quaternion.Slerp(transform.rotation, quat, turnspeed);
            Debug.Log(this.transform.rotation);
            if (Vector3.Distance(this.transform.position, target2) <= 1f)
            {
                flag = 0;
            }
        }
    }

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<Transform>();
        bullet=(GameObject)Resources.Load("Prefabs/Bullet");
        origin = this.transform.position;
        rigi = this.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        Alert();
        timeval += Time.deltaTime;

        if (AlertIsTrue)
        {
            Turning();
            if (timeval >= Attackcd)
            {
                if (AlertIsTrue)
                {
                    Attack();
                }
            }
        }
        else
        {
            Wander();
        }
        
	}

    void Turning()
    {
        Playertans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Vector3 dire = Playertans.position - this.transform.position;
        Quaternion rota = Quaternion.LookRotation(dire);
        this.GetComponent<Rigidbody>().MoveRotation(rota);
    }
    
    void Attack()
    {
        bulletrigi = Instantiate(bullet, this.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        Vector3 direction = player.position - this.transform.position;
        Debug.Log(bulletrigi);
        bulletrigi.AddForce(direction.normalized * bulletSpeed);
        //Debug.Log(direction);
       // bulletrigi.velocity = direction.normalized * bulletSpeed;
       // Debug.Log(bulletrigi.velocity);
        timeval = 0;
    }
}
