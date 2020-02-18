using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLOS : MonoBehaviour
{
    public Transform RayStart;
    public float maxDis;
    public GameObject Target;
    public Vector3 LastSeen;
    public bool canSee = false;


    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.DrawRay(RayStart.position, (other.gameObject.transform.position - RayStart.position), Color.magenta);
        if (other.gameObject.tag == "Player")
        {
            Player check = other.GetComponent<Player>();
            Target = check.gameObject;
            if (!check.GetDead()) {
                // Cast a ray straight down.
                RaycastHit2D hit = Physics2D.Raycast(RayStart.position, (other.gameObject.transform.position - RayStart.position), maxDis);
                RaycastHit2D hit2 = Physics2D.Raycast(RayStart.position, ((other.gameObject.transform.position + Vector3.up) - RayStart.position), maxDis);
                RaycastHit2D hit1 = Physics2D.Raycast(RayStart.position, ((other.gameObject.transform.position - Vector3.up) - RayStart.position), maxDis);

                Debug.DrawRay(RayStart.position, ((other.gameObject.transform.position + Vector3.up) - RayStart.position), Color.magenta);
                Debug.DrawRay(RayStart.position, ((other.gameObject.transform.position - Vector3.up) - RayStart.position), Color.magenta);
                //print(hit.collider.GetComponent<Transform>().gameObject);
                //print(hit1.collider.gameObject.tag);
                //print(hit2.collider.gameObject.tag);

                // If it hits something...
                if ((hit.collider != null && hit.collider.gameObject.tag == "Player") || (hit1.collider != null && hit1.collider.gameObject.tag == "Player") || (hit2.collider != null && hit2.collider.gameObject.tag == "Player"))
                {
                    print("HIT!");
                    LastSeen = hit.collider.gameObject.transform.position;
                    canSee = true;
                    //check.SetDead();
                }
                else
                {
                    canSee = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Target = null;
            canSee = false;
        }
    }
}
