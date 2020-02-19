using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder : MonoBehaviour
{
    public Transform ladderTop;
    public float climbSpeed = 3;

    public bool attached = false;
    void OnTriggerStay2D(Collider2D col)
    {

        if(col.gameObject.tag == "Player" && !col.gameObject.GetComponent<Player>().GetDead())
        {
            col.gameObject.GetComponent<PlayerMovement>().attached = attached;

            attached = Input.GetButton("Interact");


            if (attached)
            {
                col.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                col.gameObject.transform.position = new Vector2(ladderTop.position.x, Mathf.Min(col.gameObject.transform.position.y, ladderTop.transform.position.y));
            }
            else
            {
                col.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            }

        }else if(col.gameObject.tag == "Player" && col.gameObject.GetComponent<Player>().GetDead())
        {
            attached = false;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !other.gameObject.GetComponent<Player>().GetDead())
        {
            other.gameObject.GetComponent<PlayerMovement>().climbSpeed = climbSpeed;
            UI_InvFinder.me.nearItem = true;
            UI_InvFinder.me.messageText.text = "Hold E to climb";
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            col.gameObject.GetComponent<PlayerMovement>().attached = false;
            UI_InvFinder.me.nearItem = false;
        }
    }
}
