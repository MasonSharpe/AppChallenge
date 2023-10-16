using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewPauseMenu : MonoBehaviour
{

    public static NewPauseMenu instance;
    public Canvas canvas;


    private void Awake() {
        instance = this;
    }

    void Start()
    {

        canvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale > 0)
            {
                Time.timeScale = 0;
                canvas.enabled = true;
                Fade.instance.SetFilter(true);
            } else {
                Resume();
            }
            


        }

    }

    public void Resume()
    {
        Time.timeScale = 1;
        canvas.enabled = false;
        Fade.instance.SetFilter(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Fade.instance.SetFilter(false);
        GameManager.Respawn();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        Fade.instance.SetFilter(false);
        canvas.enabled = false;
    }

    
}
