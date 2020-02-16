using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InvFinder : MonoBehaviour
{
    public static UI_InvFinder me;
    public Image ItemImage;
    public Text ItemText;
    public Text messageText;
    public bool nearItem;

    private void Start()
    {
        if(me == null)
        {
            me = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Update()
    {
        messageText.enabled = nearItem;
    }

    public void EquipItem(Item item)
    {
        ItemImage.sprite = item.myUI;
        ItemText.text = item.Name;
    }

}
