using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;



    void Start()
    {
        Fade.instance.battleMusic.Stop();
        Fade.instance.exploreMusic.Stop();

        /*resolutions = Screen.resolutions; 

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void Update()
    {
        
    }

    public void SetResolution (int resulationIndex)
    {
        Resolution resolution = resolutions[resulationIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        */
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void EraseGameData() {
        GameManager.saveHealth = 30;
        GameManager.savePosition = new Vector3(8, 5, 0);
        GameManager.saveLevel = 1;
        GameManager.saveXP = 0;
        GameManager.saveArmor = 0;
        GameManager.saveUpgrades = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        GameManager.saveEnemiesAlive = new int[0];
        GameManager.savePickupsLeft = new int[0];
    }
}
