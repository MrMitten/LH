using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLOS : MonoBehaviour
{
    public Transform RayStart;
    public float maxDis;
    public GameObject Target;
    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.DrawRay(RayStart.position, (other.gameObject.transform.position - RayStart.position), Color.magenta);
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth check = other.GetComponent<PlayerHealth>();
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
                    print(hit.collider.gameObject);
                    print("HIT!");
                    check.SetDead();
                    Target = check.gameObject;
                    //check.SetDead();
                }
            }
        }
    }
}
