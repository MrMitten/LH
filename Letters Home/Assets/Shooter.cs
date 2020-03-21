using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    public Image img;
    public Player play;
    // Start is called before the first frame update
    void Start()
    {
        play = GetComponent<DeathDetector>().Trigger;
    }

    // Update is called once per frame
    void Update()
    {
        if (play.CanShoot)
        {
            img.enabled = true;
            img.gameObject.transform.position = Input.mousePosition;
        }
        else
            img.enabled = false;
    }
}
