using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyLOS los;
    Patrol patrol;

    public float domeTimer = 1.0f;
    public GameObject Target;
    public float ganderSpeed;
    [HideInInspector]
    public bool isWaiting = false;
    public bool isLooking = false;
    public float lookTime = 5.0f;
    private float ltimer = 0.0f;
    private Vector2 prePos;

    // Start is called before the first frame update
    void Start()
    {
        los = GetComponent<EnemyLOS>();
        patrol = GetComponent<Patrol>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (los.canSee && !isWaiting && los.Target != null && !los.Target.GetComponent<Player>().GetDead())
        {
            Target = los.Target;
            patrol.isPatroling = false;
            patrol.isFollowing = true;
            Invoke("ShootTarget", domeTimer);
            isWaiting = true;
            isLooking = false;
        }
        else if (!isWaiting && Target != null && !isLooking)
        {
            Target = null;
            patrol.isFollowing = false;
        }

        if(los.canSee == false && isLooking && Target != null && ltimer > (Time.time + lookTime/2))
        {
            transform.position = Vector2.Lerp(new Vector2(los.LastSeen.x, prePos.y), prePos, (ltimer - (Time.time + (lookTime/2)))/(lookTime/2));
        }
        else if(los.canSee == false && isLooking && Target != null)
        {
            // Looking Animation
            // TODO
        }
        print(los.canSee);
    }

    void ShootTarget()
    {
        
        if (los.canSee)
        {
            Target.GetComponent<Player>().SetDead();
            Debug.Log("KiLL!");
            patrol.isFollowing = false;
        }
        else
        {
            isLooking = true;
            Invoke("StopTheLookin", lookTime);
            prePos = transform.position;
            ltimer = Time.time + lookTime;
        }
        isWaiting = false;
    }

    void StopTheLookin()
    {
        isLooking = false;
    }
}
