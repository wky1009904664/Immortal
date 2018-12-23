using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Alert1 : MonoBehaviour {
    [Range(0, 10)]
    public float AlertRadius;

    [Range(0, 360)]
    public float Alertangle;


    private bool AlertIsTrue = false;
    public Vector3 wanderDirection;
    public float wanderDistance;
    public float walkspeed;
    public float bulletSpeed;
    public float Attackcd;
    public float turnspeed;
    public float followDistance;

    int Health=70;
    Rigidbody rigi;
    NavRun agent;
    Transform Playertans;
    Transform player;
    Vector3 origin;
    GameObject bullet;
    GameObject lightt;
    Rigidbody bulletrigi;
    Vector3 target;

    float timeval = 1;
    int flag = 0;
    int state = 0;
  //  private void OnDrawGizmos()
  //  {
  //      Color color = Handles.color;
  //      Color newww = new Color(255, 255, 255, 100);
  //      Handles.color = newww;
  //      Vector3 StartLine = Quaternion.Euler(0, -Alertangle, 0) * this.transform.forward;
  //      Handles.DrawSolidArc(this.transform.position, this.transform.up, StartLine, Alertangle, AlertRadius);
  //      Handles.color = newww;
  //  }


    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<Transform>();
        bullet=(GameObject)Resources.Load("Prefabs/EnemyBullet");
        lightt = (GameObject)Resources.Load("Prefabs/Light");
        origin = this.transform.position;
        rigi = this.GetComponent<Rigidbody>();
         agent = this.GetComponent<NavRun>();
        target = origin + wanderDirection * wanderDistance;
    }
	
	// Update is called once per frame
	void Update () {
        if (Health <= 0)
            Die();
        

        switch (state)
        {
            case 0://巡逻
                Wander();
                Alert();
                break;
            case 1://追击
                timeval += Time.deltaTime;
                Attack();
                Follow();
                break;
            case 2://回到初始点
                Return();
                break;
        }
	}

    void Alert()
    {
        Vector3 dis = player.position - this.transform.position;
        float distance = dis.magnitude;
        float disAngle = Vector3.Angle(dis, this.transform.forward);
        if (distance <= AlertRadius && disAngle <= Alertangle)
        {
            state = 1;
        }
    }

    void Wander()
    {

       
        if (flag == 0)//朝1走
        {
            agent.NavAgent(target);
            // //rigi.velocity = wanderDirection.normalized * walkspeed;
            // this.transform.Translate((wanderDirection) * walkspeed * Time.deltaTime, Space.World);
            // Quaternion quat = Quaternion.LookRotation(target1 - this.transform.position);
            // this.transform.rotation = Quaternion.Slerp(transform.rotation, quat, turnspeed);
            if (Vector3.Distance(this.transform.position, target) <= 0.1f)
            {
                flag = 1;
            }
        }
        if (flag == 1)
        {
            agent.NavAgent(origin);
            // this.transform.Translate(-wanderDirection * walkspeed * Time.deltaTime, Space.World);
            // Quaternion quat = Quaternion.LookRotation(target2 - this.transform.position);
            // Quaternion rota = Quaternion.Euler(0, 90, 0);
            // this.transform.rotation = Quaternion.Slerp(transform.rotation, quat, turnspeed);
            if (Vector3.Distance(this.transform.position, origin) <= 0.1f)
            {
                flag = 0;
            }
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
        if (timeval >= Attackcd)
        {
            bulletrigi = Instantiate(bullet, this.transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity).GetComponent<Rigidbody>();
            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;
            bulletrigi.AddForce(direction.normalized * bulletSpeed);
            //Debug.Log(direction);
            // bulletrigi.velocity = direction.normalized * bulletSpeed;
            // Debug.Log(bulletrigi.velocity);
            timeval = 0;
        }
    }

    public void DecreaseHealth()
    {
        Health -= 20;
    }

    void Follow()
    {
        agent.NavAgent(player.position);
        if ((this.transform.position - player.position).magnitude >= followDistance)
        {
            state = 2;
        }
    }

    void Return()
    {
        float dis1 = (this.transform.position - origin).magnitude;
        float dis2 = (this.transform.position - target).magnitude;
        agent.NavAgent(dis1 < dis2 ? origin : target);
        if ((this.transform.position - player.position).magnitude <= AlertRadius)
        {
            state = 1;
            return;
        }
        else if((this.transform.position - origin).magnitude <= 0.5f|| (this.transform.position - target).magnitude<=0.5f)
        {
            state = 0;
        }
    }

    void Die()
    {
        float dis = player.position.y - this.transform.position.y;
        Instantiate(lightt, this.transform.position + new Vector3(0, dis, 0), Quaternion.identity);
        Destroy(this.gameObject);
    }

}
