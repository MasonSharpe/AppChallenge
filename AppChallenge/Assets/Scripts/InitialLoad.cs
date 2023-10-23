using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class InitialLoad : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;

    private void Start()
    {
        DontDestroyOnLoad(this);
        Cursor.visible = false;

        SceneManager.LoadScene("StartingCutscene");
        Fade.instance.battleMusic.Stop();
        Player.instance.enabled = false;
        Sword.instance.enabled = false;

        myMixer.SetFloat("music", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
        myMixer.SetFloat("SFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume")) * 20); 
        

    }


}
