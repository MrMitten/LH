using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Test");
        if(other.gameObject.tag == "Player") {

            other.gameObject.GetComponent<PlayerHealth>().SetDead();
            print("Got Domed.");
        }

    }
}
