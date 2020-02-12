using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    private static float musicVolume;
    private static float SFXVolume;
    private static float NarrationVolume;
    private static bool isFullscreen;
    private AudioMixer audioMixer;
    private Resolution[] resolutions;
    public Dropdown resolutionDropdown;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + "x" + resolutions[i].height);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Getters & Setters

    void setMusicVolume(float v)

    {
        musicVolume = v;
        audioMixer.SetFloat("Volume", v);
    }

    void setSFXVolume(float v)
    {
        SFXVolume = v;
        audioMixer.SetFloat("Volume", v);
    }

    void setNarrationVolume(float v)
    {
        NarrationVolume = v;
        audioMixer.SetFloat("Volume", v);
    }

    private void setIsFullscreen(bool x)
    {
        isFullscreen = x;
    }

    float getMusicVolume()
    {
        return musicVolume;
    }

    float getSFXVolume()
    {
        return SFXVolume;
    }

    float getNarrationVolume()
    {
        return NarrationVolume;
    }
    bool getIsFullscreen()
    {
        return isFullscreen;
    }

    void setQuality(int q)
    {
        QualitySettings.SetQualityLevel(q);
    }


    #endregion

    void setScreenSize(int r)
    {
        Resolution res = resolutions[r];
        Screen.SetResolution(res.width, res.height, isFullscreen);
    }

    void toggleFullscreen()
    {
        setIsFullscreen(!isFullscreen);
        Screen.fullScreen = isFullscreen;
    }
}
