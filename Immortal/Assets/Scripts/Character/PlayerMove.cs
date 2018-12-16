using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public enum RotationAxes //枚举类
    {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXandY;

    public float sensitivityHor = 2.0f;
    public float sensitivityVert = 9.0f;

    public float minVert = -45.0f;
    public float maxVert = 45.0f;

    private float _rotationX = 0;
 
    public float speed = 80;
    public float rotateSpeed = 70;
    public float force = 20000;
    public float gravity=50000;

    private Camera camera;
    private int jump;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        jump = 0;
        camera = Camera.main;
    }

    private void aboutEyes()
    {
        if (Input.GetKey(KeyCode.E))
            camera.transform.Rotate(new Vector3(-10, 0, 0) * Time.deltaTime);  //以X轴作为旋转轴，负方向
        
        if (Input.GetKey(KeyCode.Q))
            camera.transform.Rotate(new Vector3(10, 0, 0) * Time.deltaTime);

        if  ((transform.position.y>65)&&(transform.position.y<68)) jump = 1;
        if (Input.GetKeyDown(KeyCode.Space)&& jump<2)
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0) * force);
            jump++;
        }
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0, -1, 0) * gravity * Time.deltaTime);
        Debug.Log(transform.position.y);
    }

    public void setJump()
    {
        jump = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //控制移动
        float h = Input.GetAxis("Horizontal");  //水平的  左和右
        float v = Input.GetAxis("Vertical");    //竖直的  前和后
        float xRot = Input.GetAxis("Mouse Y");
        float yRot = Input.GetAxis("Mouse X");
        aboutEyes();

        if ((h!=0)||(v!=0)) transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime);
      //  if(h!=0)
         //   transform.Rotate(Vector3.up * h*rotateSpeed * Time.deltaTime);

        //水平旋转就是以Y轴作为旋转轴旋转，鼠标移动量为偏移量
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
            //通过“增加”旋转角度进行旋转（X,Y,Z为对应方向的增加量）,一般用于无限制旋转
        }
        else if (axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);//限制_rotationX的值在minVert与minVert之间
            float rotationY = transform.localEulerAngles.y;
            //Debug.Log ("rotationX:"+_rotationX+","+Input.GetAxis ("Mouse Y") * sensitivityVert);
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            //直接“设置”旋转角度进行旋转，一般用于有限制旋转
        }
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}
