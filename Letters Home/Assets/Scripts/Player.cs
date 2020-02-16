using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool isDead = false;
    PlayerMovement moveyBoi;

    public Item myItem;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("Using Item!");
            InvokeItem();
        }


    }

    public void EquipItemPlayer(Item toQuip)
    {
        if(myItem != null)
        {
            myItem.Dequip();
            myItem.gameObject.transform.position = transform.position + Vector3.down;
            myItem.gameObject.SetActive(true);
        }
        myItem = toQuip;
        myItem.Equip();
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

    public void InvokeItem()
    {
        if(myItem != null)
        {
            myItem.UseItem();
        }
    }

}
