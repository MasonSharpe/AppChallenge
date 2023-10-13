using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMloader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Loadgame()
    {
        Time.timeScale = 1;
        Player.instance.enabled = true;
        Sword.instance.enabled = true;
        SceneManager.LoadScene("_FullMap");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}