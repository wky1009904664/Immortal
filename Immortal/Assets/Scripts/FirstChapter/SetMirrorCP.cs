using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 镜子管理脚本 —— 挂在MirrorCamera上
/// </summary>

public class SetMirrorCP : MonoBehaviour {
    public GameObject mirrorPlane;      //镜子
    private Camera mainCamera;   //主摄像机
    private Camera mirrorCamera;         //镜像摄像机

    void Start () {
        mirrorCamera = GetComponent<Camera>();
        mainCamera = Camera.main.GetComponent<Camera>();
    }
	
    //将mirrorCamera的位置设为mainCamera的位置    tmp=mirrorPlane;
	void Update () {
        if (null == mirrorPlane || null == mirrorCamera || null == mainCamera) return;
        Vector3 postionInMirrorSpace = mirrorPlane.transform.InverseTransformPoint(mainCamera.transform.position); 
        //transform position from world space to localspace 
        //将主摄像机的世界坐标位置转换为镜子的局部坐标位置

        postionInMirrorSpace.y = -postionInMirrorSpace.y;
        //一般y为镜面的法线方向
        mirrorCamera.transform.position = mirrorPlane.transform.TransformPoint(postionInMirrorSpace);
        //transform position from localspace to world space
        //把主摄像机的位置给 镜像摄像机
    }

}

