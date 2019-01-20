using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavRun : MonoBehaviour {

    private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void NavAgent(Vector3 target)
    {
        if(agent != null)
        {
            agent.SetDestination(target);
        }
    }
}
