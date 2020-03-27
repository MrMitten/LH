using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterPopup : MonoBehaviour
{

    public GameObject letterUI;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        letterUI.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        if(player.dead() == true) // if player == DEAD
        {
            letterUI.SetActive(true);

        }
    }
}
