using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public bool isPaused = false;
    public GameObject Player;
    public GameObject pauseUI;
    private static Vector3 playerPosition;
    private static PlayerMovement pm;

    private void Awake()
    {
        pm = Player.GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0.1f;
        isPaused = true;
        playerPosition = pm.transform.localPosition;
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        pm.transform.localPosition = playerPosition; 
    }
}
