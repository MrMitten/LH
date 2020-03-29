﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    public AudioClip ThunderClap;
    public AudioClip Rain;
    public AudioSource aud;
    public Light Lighting;
    public GameObject RainEffect;
    private float RandomTimer;
    public bool Raining;
    // Start is called before the first frame update
    void Start()
    {
        RandomTimer = Time.time + 5f;
        aud.clip = Rain;
    }

    // Update is called once per frame
    void Update()
    {
        if (Raining)
        {
            if (RandomTimer < Time.time)
            {
                RandomTimer = Time.time + Random.Range(5, 100);

                 aud.PlayOneShot(ThunderClap);
                 Lighting.intensity = 3f;
                 Invoke("TOff1", 0.2f);
            }
            if (!RainEffect.activeInHierarchy)
            {
                RainEffect.SetActive(true);
                aud.Play();
            }
        }else if(!Raining && RainEffect.activeInHierarchy)
        {
            RainEffect.SetActive(false);
            aud.Stop();
        }
    }

    void TOff1()
    {
        Lighting.intensity = 0;
        Invoke("TOn2", 0.2f);
    }
    void TOn2()
    {
        Lighting.intensity = 3f;
        Invoke("TOff3", 0.1f);
    }
    void TOff3()
    {
        Lighting.intensity = 0;
        Invoke("RainSound", 4f);
    }

    void RainSound()
    {
        aud.clip = Rain;
        aud.Play();
    }
}
