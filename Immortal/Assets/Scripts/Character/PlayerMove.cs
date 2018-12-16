using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    private Animator anim;
    public float speed = 80;
    public float rotateSpeed = 70;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //控制移动
        float h = Input.GetAxis("Horizontal");  //水平的  左和右
        float v = Input.GetAxis("Vertical");    //竖直的  前和后

        transform.Translate(new Vector3(0, 0, v) * speed * Time.deltaTime);
        if(h!=0)
            transform.Rotate(Vector3.up * h*rotateSpeed * Time.deltaTime);

        //  GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3(h, 0, v) * speed*Time.deltaTime);

    }
}
