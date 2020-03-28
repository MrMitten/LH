using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ammoCounter : MonoBehaviour
{
    GameObject player;
    public Text ammoCount;
    // Start is called before the first frame update
    void Start()
    {
        player = SmoothCam2D.findCam.GetComponent<SmoothCam2D>().Target;
    }

    // Update is called once per frame
    void Update()
    {
        ammoCount.text = player.GetComponent<Player>().ammo.ToString();
    }
}
