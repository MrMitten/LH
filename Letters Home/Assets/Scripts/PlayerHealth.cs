using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    bool isDead = false;
    PlayerMovement moveyBoi;

    // Start is called before the first frame update
    void Start()
    {
        moveyBoi = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead && moveyBoi.m_dead != true) {
            moveyBoi.m_dead = true; }



    }


    public void Reset()
    {
        isDead = false;
    }

    public void SetDead()
    {
        isDead = true;
    }

    public bool GetDead()
    {
        return isDead;
    }
}
