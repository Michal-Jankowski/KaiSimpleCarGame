using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PoliceChase : MonoBehaviour
{
    Transform target;

    PoliceStarSystem player;

    NavMeshAgent agent;

    Animator anim;

    public bool isChasing;


    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = target.GetComponent<PoliceStarSystem>();
        agent = GetComponent<NavMeshAgent>();

        if (player.isChasable) {
            isChasing = true;
        }
    }

    // Update is called once per frame
    void Update() {

        if (player.isChasable) {

            if (isChasing) {

                agent.isStopped = false;
                agent.SetDestination(target.position);
                agent.speed = 1.2f;
            }
            else {
                agent.isStopped = true;
            }

        }
        else {
            agent.isStopped = true;
        }
        
    }
}
