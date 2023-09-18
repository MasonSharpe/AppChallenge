using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewPauseMenu : MonoBehaviour
{
    public new AudioSource audioYes;
    public new AudioSource audioNo;
    // Start is called before the first frame update
    void Start()
    {
        
        GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale > 0)
            {
                Time.timeScale = 0;
                GetComponent<Canvas>().enabled = true;
            }
            else
            {
                Resume();
            }


        }

    }

    public void Resume()
    {
        Time.timeScale = 1;
        GetComponent<Canvas>().enabled = false;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        audioYes.Play();
        Debug.Log("quit");
        Application.Quit();
    }
    public void BackToPauseMenu()
    {
        audioNo.Play();
        Debug.Log("pressed");
    }
}
