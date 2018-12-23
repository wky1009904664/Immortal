using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Trip1 : MonoBehaviour {

    [Range(0, 10)]
    public float AlertRadius;

    [Range(0, 360)]
    public float Alertangle;

    float timeval = 0;
    public float Attackcd;
    Transform player;

    private void OnDrawGizmos()
    {
        Color color = Handles.color;
        Color newww = new Color(150, 105, 30, 100);
        Handles.color = newww;
        Vector3 StartLine = Quaternion.Euler(0, -Alertangle, 0) * this.transform.forward;
        Handles.DrawSolidArc(this.transform.position, this.transform.up, StartLine, Alertangle, AlertRadius);
        Handles.color = newww;
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        timeval += Time.deltaTime;
        if (timeval >= Attackcd)
        {
            Alert();
            timeval = 0;
        }
	}

    void Alert()
    {
        Vector3 dis = player.position - this.transform.position;
        float distance = dis.magnitude;
        float disAngle = Vector3.Angle(dis, this.transform.forward);
        if (distance <= AlertRadius && disAngle <= Alertangle)
        {
            player.GetComponent<PlayerMovement>().DecreaseHealth();
        }
    }
}
