using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewPauseMenu : MonoBehaviour
{
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
        Debug.Log("quit");
        Application.Quit();
    }
    
}
