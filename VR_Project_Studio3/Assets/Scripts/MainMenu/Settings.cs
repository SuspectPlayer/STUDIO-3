//Written by Jack and Jasper
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    public Dropdown fullScreenDropdown;
    public Dropdown resolutionDropdown;


    public void Start()
    {

        //Sets the saved player settings
        if(fullScreenDropdown = null)
        {
            if (PlayerPrefs.GetInt("Window", 0) == 0)
            {
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            }
            else if (PlayerPrefs.GetInt("Window", 0) == 1)
            {
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            }
            else if (PlayerPrefs.GetInt("Window", 0) == 2)
            {
                Screen.fullScreenMode = FullScreenMode.Windowed;

            }
        }
        else
        {
            fullScreenDropdown.value = PlayerPrefs.GetInt("Window", 0);
        }

        if(resolutionDropdown = null)
        {
            if (PlayerPrefs.GetInt("Resolution", 0) == 0 && (PlayerPrefs.GetInt("Window", 0) == 1 || PlayerPrefs.GetInt("Window", 0) == 0))
            {
                Screen.SetResolution(1920, 1080, true);
            }
            else if (PlayerPrefs.GetInt("Resolution", 0) == 0)
            {
                Screen.SetResolution(1920, 1080, false);
            }
            else if (PlayerPrefs.GetInt("Resolution", 0) == 1 && (PlayerPrefs.GetInt("Window", 0) == 1 || PlayerPrefs.GetInt("Window", 0) == 0))
            {
                Screen.SetResolution(1600, 900, true);
            }
            else if (PlayerPrefs.GetInt("Resolution", 0) == 1)
            {
                Screen.SetResolution(1600, 900, false);
            }
            else if (PlayerPrefs.GetInt("Resolution", 0) == 2 && PlayerPrefs.GetInt("Window", 0) == 1)
            {
                Screen.SetResolution(1280, 720, true);
            }
            else if (PlayerPrefs.GetInt("Resolution", 0) == 2)
            {
                Screen.SetResolution(1280, 720, false);
            }
        }
        else
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", 0);
        }
    }


    //Dictates what happens when the window mode is changed
    public void WindowChange()
    {
        if (fullScreenDropdown.value == 0)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else if (fullScreenDropdown.value == 1)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else if (fullScreenDropdown.value == 2)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;

        }
        PlayerPrefs.SetInt("Window", fullScreenDropdown.value);
    }
    //Dictates what happens when the resolution mode is changed
    public void ResolutionChange()
    {
        if (resolutionDropdown.value == 0 && (fullScreenDropdown.value == 1 || fullScreenDropdown.value == 0))
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else if (resolutionDropdown.value == 0)
        {
            Screen.SetResolution(1920, 1080, false);
        }
        else if (resolutionDropdown.value == 1 && (fullScreenDropdown.value == 1 || fullScreenDropdown.value == 0))
        {
            Screen.SetResolution(1600, 900, true);
        }
        else if (resolutionDropdown.value == 1)
        {
            Screen.SetResolution(1600, 900, false);
        }
        else if (resolutionDropdown.value == 2 && fullScreenDropdown.value == 1)
        {
            Screen.SetResolution(1280, 720, true);
        }
        else if (resolutionDropdown.value == 2)
        {
            Screen.SetResolution(1280, 720, false);
        }
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
    }

  
}
