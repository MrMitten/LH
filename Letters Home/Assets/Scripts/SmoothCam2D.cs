using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCam2D : MonoBehaviour
{
    public Vector2 BoundsLR = new Vector2();
    public GameObject Target;
    public Vector2 Offset;
    // Start is called before the first frame update
    void Start()
    {
        BoundsLR[0] += transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(Target.transform.position.x + Offset.x,Target.transform.position.y + Offset.y,-10), 0.02f);
        if(transform.position.x < BoundsLR[0])
        {
            transform.position = new Vector3(BoundsLR[0], transform.position.y, transform.position.z);
        }
        else if(transform.position.x > BoundsLR[1])
        {
            transform.position = new Vector3(BoundsLR[1], transform.position.y, transform.position.z);
        }
    }
}
