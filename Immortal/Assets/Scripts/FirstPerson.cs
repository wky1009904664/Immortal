using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour {
    //让相机以流行的方式晃动
    public Matrix4x4 originalProjection;
    private Camera me;
    private bool flow;

    // Use this for initialization
    void Start () {
        flow = false;
        me = GetComponent<Camera>();
        originalProjection = me.projectionMatrix;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            transform.Rotate(new Vector3(1,0,0) * -1);
        }
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            transform.Rotate(new Vector3(1, 0, 0) );
        }

        if (flow)
        {
            //改变原始矩阵的某些值
            Matrix4x4 p = originalProjection;
            p.m01 += Mathf.Sin(Time.time * 1.2F) * 0.1F;
            p.m10 += Mathf.Sin(Time.time * 1.5F) * 0.1F;
            me.projectionMatrix = p;
        }
    }

    public void setFlow()
    {
        flow = !flow;
    }
}
