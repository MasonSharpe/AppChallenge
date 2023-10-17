using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMloader : MonoBehaviour
{
    void Start()
    {
        Player.instance.enabled = false;
        Sword.instance.enabled = false;
    }



    public void Loadgame()
    {
        Time.timeScale = 1;
        Player.instance.enabled = true;
        Sword.instance.enabled = true;
        GameManager.Respawn();
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}