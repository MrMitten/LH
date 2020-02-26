﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Requires that a Nav Mesh is already baked.
/// </summary>
public class PatrolAI : MonoBehaviour
{
    // Each object needs to have a Trigger Collider and a RigidBody
    public Transform pos1;
    public Transform pos2;

    private NavMeshAgent agent;
    [HideInInspector]
    public bool reset = true;
    private float goingTo;
    [HideInInspector]
    public bool stopMoving = false;
    [HideInInspector]
    public bool isPatroling = true;
    [HideInInspector]
    public bool isFollowing = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatroling)
        {
            print(goingTo);
            if (reset)
            {
                agent.SetDestination(pos1.position);
                goingTo = 1;
                reset = false;
            }
            else if (goingTo == 1 && transform.position.x >= pos1.position.x+1)
            {
                agent.SetDestination(pos1.position);
            }
            else if (goingTo == 1 && transform.position.x <= pos1.position.x + 1)
            {
                goingTo = 2;
                transform.rotation.eulerAngles.Set(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180f, transform.rotation.eulerAngles.z);
                agent.SetDestination(pos2.position);
            }
            else if (goingTo == 2 && transform.position.x <= pos2.position.x-1)
            {
                agent.SetDestination(pos2.position);
            }
            else if (goingTo == 2 && transform.position.x >= pos2.position.x - 1)
            {
                goingTo = 1;
                transform.rotation.eulerAngles.Set(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180f, transform.rotation.eulerAngles.z);
                agent.SetDestination(pos1.position);
            }
        }
        else if (!isPatroling && stopMoving)
        {
            agent.ResetPath();
            stopMoving = false;
        }
        
    }
}
