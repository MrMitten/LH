using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public Sprite myUI;
    public string Name;

    public UnityEvent OnUse;
    public UnityEvent OnEquip;
    public UnityEvent OnDequip;

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

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            other.gameObject.GetComponent<Player>().EquipItemPlayer(this);
            UI_InvFinder.me.EquipItem(this);
            this.gameObject.SetActive(false);

        }else if(other.gameObject.tag == "Player")
        {
            UI_InvFinder.me.messageText.text = "PRESS F TO PICKUP:" + Name;
            UI_InvFinder.me.nearItem = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            UI_InvFinder.me.nearItem = false;
        }
    }

}
