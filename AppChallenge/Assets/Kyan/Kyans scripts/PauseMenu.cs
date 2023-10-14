using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
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
                Cursor.visible = true;
                GetComponent<Canvas>().enabled = true;
                Fade.instance.SetFilter(true);
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
        Cursor.visible = false;
        GetComponent<Canvas>().enabled = false;
        Fade.instance.SetFilter(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Fade.instance.SetFilter(false);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Fade.instance.SetFilter(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
