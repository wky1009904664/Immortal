using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy3Nav : MonoBehaviour {

    NavMeshAgent agent;
    Transform player;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<Transform>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update () {
        agent.SetDestination(player.position);
	}
}
