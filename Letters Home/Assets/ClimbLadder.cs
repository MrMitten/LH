using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder : MonoBehaviour
{
    public Transform ladderTop;
    public bool attached = false;
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && !col.gameObject.GetComponent<Player>().GetDead())
        {
            col.gameObject.GetComponent<PlayerMovement>().attached = attached;

            attached = Input.GetKey(KeyCode.F);


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

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            col.gameObject.GetComponent<PlayerMovement>().attached = false;
        }
    }
}
