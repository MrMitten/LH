﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public Sprite myUI;
    public string Name;
    private bool canGrab = false;

    public UnityEvent OnUse;
    public UnityEvent OnEquip;
    public UnityEvent OnDequip;
    private GameObject Player;

    private void Update()
    {
        if (canGrab && Input.GetKeyDown(KeyCode.F))
        {
            Player.GetComponent<Player>().EquipItemPlayer(this);
            UI_InvFinder.me.EquipItem(this);
            canGrab = false;
            this.gameObject.SetActive(false);
            
        }
    }

    public void UseItem()
    {
        OnUse.Invoke();
    }

    public void Equip()
    {
        OnEquip.Invoke();
    }
    public void Dequip()
    {
        OnDequip.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player = other.gameObject;
            UI_InvFinder.me.messageText.text = "PRESS F TO PICKUP:" + Name;
            UI_InvFinder.me.nearItem = true;
            canGrab = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            UI_InvFinder.me.nearItem = false;
            canGrab = false;
        }
    }

}
