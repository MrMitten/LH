using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;

    private bool movingRight = true;

    public Transform groundDetection;

    public float zDist;

    void Start()
    {
        zDist = transform.position.z;
    }
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, zDist);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        if(groundInfo.collider == false)
        {
            if(movingRight == true)
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
}
