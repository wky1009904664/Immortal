using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制主相机移动脚本 —— 实现第三人称视角
/// </summary>

public class Flow : MonoBehaviour
{
    public Transform pivot;
    private Vector3 pivotOffset = Vector3.zero;
    private float distance = 10.0f;
    private float minDistance = 2f;
    private float maxDistance = 15f;
    private float zoomSpeed = 1f;
    private float xSpeed = 250.0f;
    private float ySpeed = 120.0f;
    private bool allowYTilt = true;
    private float yMinLimit = -90f;
    private float yMaxLimit = 90f;
    private float x = 0.0f;
    private float y = 0.0f;
    private float targetX = 0f;
    private float targetY = 0f;
    private float targetDistance = 0f;
    private float xVelocity = 1f;
    private float yVelocity = 1f;
    private float zoomVelocity = 1f;
    //让相机以流行的方式晃动
    public Matrix4x4 originalProjection;
    private Camera flow;

    public void Awake()
    {
        flow = GetComponent<Camera>();
        originalProjection = flow.projectionMatrix;
    }

    void Start()
    {
        var angles = transform.eulerAngles;     //var 匿名类型
        targetX = x = angles.x;
        targetY = y = ClampAngle(angles.y, yMinLimit, yMaxLimit);
        targetDistance = distance;
    }

    void Update()
    {
        //改变原始矩阵的某些值
        Matrix4x4 p = originalProjection;
        p.m01 += Mathf.Sin(Time.time * 1.2F) * 0.1F;
        p.m10 += Mathf.Sin(Time.time * 1.5F) * 0.1F;
        flow.projectionMatrix = p;
    }

    void LateUpdate()
    {
        if (!pivot) return;
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0.0f) targetDistance -= zoomSpeed;
        else if (scroll < 0.0f) targetDistance += zoomSpeed;
        targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);
        if (Input.GetMouseButton(1) ||
            (Input.GetMouseButton(0) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
            )
        {
            targetX += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            if (allowYTilt)
            {
                targetY -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                targetY = ClampAngle(targetY, yMinLimit, yMaxLimit);
            }
        }
        x = Mathf.SmoothDampAngle(x, targetX, ref xVelocity, 0.3f);
        y = allowYTilt ? Mathf.SmoothDampAngle(y, targetY, ref yVelocity, 0.3f) : targetY;
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        distance = Mathf.SmoothDamp(distance, targetDistance, ref zoomVelocity, 0.5f);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + pivot.position + pivotOffset;
        transform.rotation = rotation; transform.position = position;

    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

}
