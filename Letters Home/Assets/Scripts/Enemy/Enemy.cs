using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    EnemyLOS los;
    PatrolAI patrol;

    [HideInInspector]
    public GameObject Target;
    private NavMeshAgent agent;

    public float domeTimer = 1.0f;
    public float lookTime = 2.0f;
    public float ganderSpeed = 0.75f;
    [HideInInspector]
    public bool isLooking = false;
    [HideInInspector]
    public bool isSearching = false;
    private float ltimer = 0.0f;
    private float originalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        los = GetComponent<EnemyLOS>();
        patrol = GetComponent<PatrolAI>();
        agent = GetComponent<NavMeshAgent>();
        originalSpeed = agent.speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        // If this enemy can see the Player, stop patrolling and get ready to shoot.
        if (los.canSee && los.Target != null && !los.Target.GetComponent<Player>().GetDead())
        {
            Target = los.Target;
            patrol.isPatroling = false;
            patrol.stopMoving = true;
            Invoke("ShootTarget", domeTimer);
        }
        // If this enemy saw the Player, but can't anymore, they will walk to the last seen location of the Player.
        else if (!los.canSee && isSearching && Target != null && transform.position.x != los.LastSeen.x) // && ltimer > (Time.time + lookTime / 2))
        {
            agent.SetDestination(los.LastSeen);
        }
        // If the enemy has reached the last seen location of the Player, wait for the look timer to run out, then go back to patrolling.
        else if (!los.canSee && isSearching && Target != null && transform.position.x == los.LastSeen.x)
        {
            isSearching = false;
            isLooking = true;
            ltimer = Time.time + lookTime;
            agent.ResetPath();
        }
        // This enemy is at the last seen location of the Player, and is looking around for them.
        else if (!los.canSee && isLooking && Target != null && ltimer > Time.time)
        {
            // TODO
            // Looking Animation
        }
        // This enemy is at the last seen location of the Player, and has finished looking for them.
        else if (!los.canSee && isLooking && Target != null && Time.time > ltimer)
        {
            patrol.reset = true;
            patrol.isPatroling = true;
            isLooking = false;
            Target = null;
            agent.speed = originalSpeed;
        }
    }

    void ShootTarget()
    {
        if (los.canSee)
        {
            // Kill the Player
            Target.GetComponent<Player>().SetDead();
            Debug.Log("KiLL!");

            // Reset patrol
            patrol.isPatroling = true;
            patrol.reset = true;
            agent.speed = originalSpeed;
        }
        else
        {
            isSearching = true;
            agent.speed = ganderSpeed;
        }
    }
    
}
