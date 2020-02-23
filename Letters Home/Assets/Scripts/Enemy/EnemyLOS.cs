using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLOS : MonoBehaviour
{
    public Transform RayStart;
    public float maxDis;
    [HideInInspector]
    public GameObject Target;
    [HideInInspector]
    public Vector3 LastSeen;
    [HideInInspector]
    public bool canSee = false;


    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {

        Debug.DrawRay(RayStart.position, (other.gameObject.transform.position - RayStart.position), Color.magenta);
        Debug.DrawRay(RayStart.position, ((other.gameObject.transform.position + Vector3.up) - RayStart.position), Color.magenta);
        Debug.DrawRay(RayStart.position, ((other.gameObject.transform.position - Vector3.up) - RayStart.position), Color.magenta);

        if (other.gameObject.tag == "Player")
        {
            Player check = other.GetComponent<Player>();
            Target = check.gameObject;
            if (!check.GetDead()) {
                
                Ray ray = new Ray(RayStart.position, (other.gameObject.transform.position - RayStart.position));
                Ray ray1 = new Ray(RayStart.position, ((other.gameObject.transform.position + Vector3.up) - RayStart.position));
                Ray ray2 = new Ray(RayStart.position, ((other.gameObject.transform.position - Vector3.up) - RayStart.position));

                RaycastHit hit, hit1, hit2;

                Physics.Raycast(ray, out hit, maxDis);
                Physics.Raycast(ray1, out hit1, maxDis);
                Physics.Raycast(ray2, out hit2, maxDis);

                // If it hits something...
                if ((hit.collider != null && hit.collider.gameObject.tag == "Player") 
                    || (hit1.collider != null && hit1.collider.gameObject.tag == "Player") 
                    || (hit2.collider != null && hit2.collider.gameObject.tag == "Player"))
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Target = null;
            canSee = false;
        }
    }
}
