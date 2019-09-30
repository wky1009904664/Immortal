using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDoor : MonoBehaviour
{

    bool canAccess = false;
    public int NextFloorIndex;
    GameObject barrier;
    Transform player;
    Animator shanlan;
    // Start is called before the first frame update
    void Start()
    {
        shanlan = transform.GetChild(1).GetComponent<Animator>();
        barrier = this.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        shanlan.SetBool("UpFence", true);
        canAccess = true;
        Destroy(barrier, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (canAccess)
                SceneManager.LoadScene(NextFloorIndex);
        }
    }
}
