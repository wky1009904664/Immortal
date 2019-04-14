using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2 : MonoBehaviour
{
     public float speed = 0f;
     bool move1 = false;
     public bool dec = false;
    public static Vector3 dire;

    // Use this for initialization
    void Start()
    {

    }

    public void Move1(Vector3 dire)
    {
        move1 = true;
        dire.y = 0;
        Debug.LogWarning(dire);
        transform.Translate(dire * speed * Time.deltaTime,Space.World);
        

        if (speed < 0) move1 = false;
        if (speed < 23 && !dec) speed += 0.5f;
        else { dec = true; speed -= 0.5f; }
    }

    // Update is called once per frame
    void Update()
    {
        if (move1) Move1(dire);
    }
}
