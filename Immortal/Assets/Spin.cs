using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public GameObject whiteCenter;
    public GameObject blackCenter;

    private float origionY; //声明初始的Y轴旋转值 
    private Quaternion whiteTargetRotation; //声明旋转目标角度 
    private Quaternion blackTargetRotation;
    public float RotateAngle = 120; //定义每次旋转的角度 
    private int whiteCount=0; //声明一个量记录到目标角度需要进行旋转RotateAngle度的个数
    private int blackCount=0;

    private void Start()
    {
        origionY = whiteCenter.transform.rotation.y; //获取当前Y轴旋转值赋给origionY
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            whiteCount++; 
            whiteTargetRotation = Quaternion.Euler(0, RotateAngle * whiteCount + origionY, 0) * Quaternion.identity; //给旋转目标值赋值, 由于只有Y轴动，
            // 所以目标值应是(旋转角*需要旋转的次数+origionY物体初始Y轴旋转角)*Quarternion.identity四元数的初始值,记住写就行
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            blackCount--;
            blackTargetRotation = Quaternion.Euler(0, RotateAngle * blackCount + origionY, 0) * Quaternion.identity; 
        }

        whiteCenter.transform.rotation = Quaternion.Slerp(whiteCenter.transform.rotation, whiteTargetRotation, Time.deltaTime * 2);
        //利用Slerp插值让物体进行旋转 2是旋转速度 越大旋转越快 
        if (Quaternion.Angle(whiteTargetRotation, whiteCenter.transform.rotation) < 0.5)
            //当物体当前角度与目标角度差值小于1度直接将目标角度赋予物体 让旋转角度精确到我们想要的度数
            whiteCenter.transform.rotation = whiteTargetRotation; 
        

        blackCenter.transform.rotation = Quaternion.Slerp(blackCenter.transform.rotation, blackTargetRotation, Time.deltaTime * 2);
        if (Quaternion.Angle(blackTargetRotation, blackCenter.transform.rotation) < 0.5)
            blackCenter.transform.rotation = blackTargetRotation;
        
    }

}
