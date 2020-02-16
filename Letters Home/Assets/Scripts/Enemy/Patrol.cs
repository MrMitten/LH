using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public bool isPatroling = true;
    public bool isFollowing = false;
    public float speed;

    private bool movingRight = true;

    public Transform groundDetection;

    public float zDist;

    private Transform startPos;
    RaycastHit2D groundInfo;

    void Start()
    {
        isPatroling = true;
        zDist = transform.position.z;
        startPos = this.transform;
    }
    void FixedUpdate()
    {
        groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        if (isPatroling)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
            transform.position = new Vector3(transform.position.x, transform.position.y, zDist);
            
            if (groundInfo.collider == false)
            {
                if (movingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }
        }
        else if(isFollowing == false)
        {
            getBackToRoute();
        }
        

    }

    private void getBackToRoute()
    {
        if (groundInfo.collider == false)
        {
            //Lerp back to the start position, then turn patrolling back on
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        else
        {
            isPatroling = true;
        }
    }
}
